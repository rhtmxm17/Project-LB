using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 스테이지 매니저는 StageData를 기반으로 스테이지 씬을 불러오고 초기화합니다<br/>
/// 일지, 설계도, 클리어 트리거는 태그로 검색해서 초기화합니다...
/// </summary>
public class StageSceneManager : MonoBehaviour
{
    /*
     스테이지 매니저에서 처리할 목록

    생성시 필요한 데이터
        스테이지별 고정된 데이터 (StageData : ScriptableObject)
            지형 데이터
            레벨 디자인 데이터
            보상
            수류탄 스펙
        플레이어
            체력 혹은 레벨
        선택된 무기 목록
            기본 무기 레벨
        
    UI 띄우기 -> 해당 UI 작업 담당자와 논의 필요
        클리어 이벤트 구독해서 UI 띄우기
        플레이어 사망 이벤트 구독해서 게임오버 UI 띄우기
    스테이지 종료시 데이터 관리자에 결과 통지(획득한 재화 및 경험치, 수집품)

    스테이지 진입 방식
    벙커 씬에서 StageSceneManager 준비 -> StageData 등록 -> EnterStage 호출



     */

    [SerializeField] StageData stageDataTable;
    public StageData StageDataTable
    {
        get => stageDataTable;
        private set
        {
            Debug.Log($"[StageSceneManager] 새로운 스테이지 데이터가 설정됨: {stageDataTable.StageName}->{value.StageName}");
            stageDataTable = value;
        }
    }

    public readonly ItemType[] weaponEquip = new ItemType[3] { ItemType.AK47, ItemType.KNIFE, ItemType.NONE };

    public bool HasJournal { get; private set; }

    public bool HasBlueprint { get; private set; }

    public UnityEvent<bool> OnStageClear;

    /// <summary>
    /// 해당 스테이지 씬으로 진입
    /// </summary>
    [ContextMenu("EnterStage Test")]
    private void EnterStage()
    {
        SceneChanger sceneChanger = GameManager.Instance.GetSceneChanger();
        sceneChanger.OnLoadSceneComplete.AddListener(InitStage);
        sceneChanger.ChangeToMultiScene(StageDataTable.MapScene, StageDataTable.LevelScene);
    }

    public void EnterStage(StageData stageData)
    {
        StageDataTable = stageData;
        EnterStage();
    }

    public void EnterStage(ItemType mainWeapon, ItemType meleeWeapon, ItemType specialWeapon, StageData stageData)
    {
        weaponEquip[0] = mainWeapon;
        weaponEquip[1] = meleeWeapon;
        weaponEquip[2] = specialWeapon;
        StageDataTable = stageData;
        EnterStage();
    }


    /// <summary>
    /// 스테이지 클리어시 호출될 함수<br/>
    /// 예시)일반 스테이지의 최종 도착 위치 트리거 박스에 구독
    /// </summary>
    public void StageCleared()
    {
        PlayerData playerData = GameManager.Instance.GetPlayerData();

        // 클리어 카운트 증가
        playerData.stageClearCntArr[stageDataTable.StageIndex]++;

        // 수집품 획득 처리
        if (HasJournal)
        {
            ItemData journal = playerData.GetItemData(StageDataTable.Journal);
            journal.count = 1;
        }

        if (HasBlueprint)
        {
            ItemData blueprint = playerData.GetItemData(StageDataTable.BluePrint);
            blueprint.count = 1;
        }

        // 재화 획득 처리
        bool isLevelUp = playerData.AddExp(StageDataTable.RewardExp);
        playerData.AddFood(StageDataTable.RewardRation);

        OnStageClear?.Invoke(isLevelUp);
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void InitStage()
    {
        // TODO: BGM 재생

        PlayerModel player = GameManager.Instance.GetPlayerModel();
        if (player == null)
        {
            Debug.LogError("플레이어 캐릭터를 찾을 수 없음");
            return;
        }

        if (! player.TryGetComponent(out StagePlayerControl playerControl))
        {
            Debug.LogError("스테이지용 플레이어 캐릭터가 아님");
            return;
        }

        // 플레이어 초기화
        PlayerData playerData = GameManager.Instance.GetPlayerData();


        StagePlayerControl.StageInitAttribute playerInitAttr = new()
        {
            maxHp = playerData.MaxHP,
            mainWeapon = InitWeapon(weaponEquip[0]),
            meleeWeapon = InitWeapon(weaponEquip[1]),
            specialWeapon = InitWeapon(weaponEquip[2]),
            grenadeData = StageDataTable.Grenade,
        };

        playerControl.StageInit(playerInitAttr);

        // 클리어 이벤트를 찾아서 StageCleared 구독
        TriggerArea ClearTrigger = GameObject.FindWithTag("ClearTrigger")?.GetComponent<TriggerArea>();
        if (ClearTrigger == null)
        {
            Debug.Log("스테이지에 클리어 조건이 존재하지 않음");
        }
        else
        {
            ClearTrigger.onTriggerEvent.AddListener(StageCleared);
        }

        // TODO: HUD에 플레이어 정보 등록
        //  태그 등록 + 인터페이스 구현 또는 TriggerArea 타입 참조 사용
    }

    private GunBase InitWeapon(ItemType type)
    {
        if (type == ItemType.NONE)
            return null;

        InventoryEquableItemSO gunTable = (InventoryEquableItemSO)GameManager.Instance.GetItemDataTable().GetItemDataSO(type);
        GunBase weapon = Instantiate(gunTable.GunPrefab);
        weapon.DataTable = gunTable;
        weapon.GunLevel = GameManager.Instance.GetPlayerData().GetItemData(type).WeaponLevel;
        return weapon;
    }

    public void InitBluePrint(Collection collecterItem)
    {
        // 플레이어가 해당 설계도를 소지중인지 검사
        if (0 < GameManager.Instance.GetPlayerData().GetItemData(StageDataTable.BluePrint).count)
        {
            Destroy(collecterItem.gameObject);
        }
        else
        {
            collecterItem.OnPickup.AddListener(() => { HasBlueprint = true; });
        }
    }

    public void InitJournal(Collection collecterItem)
    {
        // 플레이어가 해당 저널을 소지중인지 검사
        if (0 < GameManager.Instance.GetPlayerData().GetItemData(StageDataTable.Journal).count)
        {
            Destroy(collecterItem.gameObject);
        }
        else
        {
            collecterItem.OnPickup.AddListener(() => { HasJournal = true; });
        }
    }
}