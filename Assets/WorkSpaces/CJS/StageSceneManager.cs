using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
        set
        {
            Debug.Log($"[StageSceneManager] 새로운 스테이지 데이터가 설정됨: {stageDataTable.StageName}->{value.StageName}");
            stageDataTable = value;
        }
    }


    public bool GetJournal { get; set; }

    public bool GetBlueprint { get; set; }

    /// <summary>
    /// 해당 스테이지 씬으로 진입
    /// </summary>
    [ContextMenu("EnterStage Test")]
    public void EnterStage()
    {
        SceneChanger sceneChanger = GameManager.Instance.GetSceneChanger();
        sceneChanger.OnLoadSceneComplete.AddListener(InitStage);
        sceneChanger.ChangeToMultiScene(StageDataTable.MapScene, StageDataTable.LevelScene);
    }

    /// <summary>
    /// 스테이지 클리어시 호출될 함수<br/>
    /// 예시)일반 스테이지의 최종 도착 위치 트리거 박스에 구독
    /// </summary>
    public void StageCleared()
    {
        PlayerData playerData = GameManager.Instance.GetPlayerData();

        // 수집품 획득 처리
        if (GetJournal)
        {
            ItemData journal = playerData.GetItemData(StageDataTable.Journal);
            journal.count = 1;
        }

        if (GetBlueprint)
        {
            ItemData blueprint = playerData.GetItemData(StageDataTable.BluePrint);
            blueprint.count = 1;
        }

        // 재화 획득 처리
        playerData.AddExp(StageDataTable.RewardExp);
        playerData.AddFood(StageDataTable.RewardRation);

        // TODO: 클리어 UI 출력, UI 버튼에 씬 전환 메서드 추가
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void InitStage()
    {
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
        ItemType mainWeaponType = ItemType.AK47;          //
        ItemType meleeWeaponType = ItemType.KNIFE;        //
        ItemType specialWeaponType = ItemType.SP_WEAPON1; // TODO: 실제로 선택된 아이템 가져오기


        StagePlayerControl.StageInitAttribute playerInitAttr = new()
        {
            maxHp = playerData.MaxHP,
            mainWeapon = InitWeapon(mainWeaponType),
            meleeWeapon = InitWeapon(meleeWeaponType),
            specialWeapon = InitWeapon(specialWeaponType),
            grenadeData = StageDataTable.Grenade,
        };

        playerControl.StageInit(playerInitAttr);

        // TODO: 클리어 이벤트를 찾아서 StageCleared 구독
        //  태그 등록 + 인터페이스 구현 또는 TriggerArea 타입 참조 사용
    }

    private GunBase InitWeapon(ItemType type)
    {
        InventoryEquableItemSO gunTable = (InventoryEquableItemSO)GameManager.Instance.GetItemDataTable().GetItemDataSO(type);
        GunBase weapon = Instantiate(gunTable.GunPrefab);
        weapon.DataTable = gunTable;
        weapon.GunLevel = GameManager.Instance.GetPlayerData().GetItemData(type).WeaponLevel;
        return weapon;
    }
}