using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 스테이지 매니저는 StageData를 기반으로 스테이지 씬을 불러오고 초기화합니다<br/>
/// 클리어 트리거는 태그로 검색해서 초기화합니다...
/// </summary>
public class StageSceneManager : MonoBehaviour
{
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

    /// <summary>
    /// 획득한 일지 id를 플래그로 저장
    /// </summary>
    private int journalFlag = 0;

    public bool HasBlueprint { get; private set; }

    public UnityEvent<bool> OnStageClear;

    /// <summary>
    /// 해당 스테이지 씬으로 진입
    /// </summary>
    [ContextMenu("EnterStage Test")]
    private void EnterStage()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // 로딩 BGM 필요시 교체
        GameManager.Instance.GetSoundManager().StopBGM();

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

        // 클리어 카운트 증가 및 최근 임무 성공 여부 기록
        playerData.stageClearCntArr[stageDataTable.StageIndex]++;
        playerData.isStageCleared = true;

        // 수집품 획득 처리
        //if (HasJournal)
        //{
        //    playerData.AddItem(StageDataTable.Journal);
        //}

        // 일지 획득 플래그가 있다면
        if (journalFlag != 0)
        {
            for (int i = (int)ItemType.PAGE1; i <= (int)ItemType.PAGE6; i++)
            {
                // 적중한 플래그의 아이템 획득
                if (0 != (journalFlag & i))
                {
                    playerData.AddItem((ItemType)i);
                }
            }
        }

        if (HasBlueprint)
        {
            playerData.AddItem(StageDataTable.BluePrint);
        }

        // 재화 획득 처리
        bool isLevelUp = playerData.AddExp(StageDataTable.RewardExp);
        playerData.AddFood(StageDataTable.RewardRation);

        OnStageClear?.Invoke(isLevelUp);
    }

    private void ExitStage()
    {
        Debug.Log($"ExitStage");
        SceneChanger sceneChanger = GameManager.Instance.GetSceneChanger();
        sceneChanger.ChangeScene(SceneChanger.Scenes.BUNCKER);
        Time.timeScale = 1f;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void InitStage()
    {
        // BGM 재생
        if (StageDataTable.BgmClip != null)
        {
            GameManager.Instance.GetSoundManager().PlayBGM(StageDataTable.BgmClip);
        }

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

        player.GetComponent<PlayerCharacterControllerControl>().MouseLock(true);

        // 플레이어 초기화
        PlayerData playerData = GameManager.Instance.GetPlayerData();
        playerData.isStageCleared = false;


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

        Time.timeScale = 1f;
    }

    private GunBase InitWeapon(ItemType type)
    {
        if (type == ItemType.NONE)
            return null;

        InventoryEquableItemSO gunTable = (InventoryEquableItemSO)GameManager.Instance.GetItemDataTable().GetItemDataSO(type);
        GunBase weapon = Instantiate(gunTable.GunPrefab);
        weapon.GunLevel = GameManager.Instance.GetPlayerData().GetItemData(type).WeaponLevel;
        weapon.DataTable = gunTable;
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

    public void InitJournal(Diary collecterItem)
    {
        // 플레이어가 해당 저널을 소지중인지 검사
        if (0 < GameManager.Instance.GetPlayerData().GetItemData(collecterItem.ItemID).count)
        {
            Destroy(collecterItem.gameObject);
        }
        else
        {
            collecterItem.OnPickup.AddListener(() => { journalFlag |= (1 << (int)collecterItem.ItemID); });
        }
    }

    public void InitClearUI(StageClearUI ui)
    {
        ui.SetFoodGain($"{stageDataTable.RewardRation}");
        ui.SetGearGain($"{stageDataTable.RewardExp}");
    }

    public void InitGameOverUI(GameOverUI ui)
    {
        ui.OnReturnButtonClicked.AddListener(ExitStage);
        // TODO: 재도전 버튼
    }

    public Sprite InitQuickSlotSprite(int index)
    {
        switch (index)
        {
            case 0:
                return GameManager.Instance.GetItemDataTable().GetItemDataSO(weaponEquip[0]).ImgSprite;
            case 1:
                return GameManager.Instance.GetItemDataTable().GetItemDataSO(weaponEquip[1]).ImgSprite;
            case 4:
                return GameManager.Instance.GetItemDataTable().GetItemDataSO(weaponEquip[2]).ImgSprite;
            default:
                Debug.LogWarning("퀵슬롯 번호가 잘못됨");
                return null;
        }
    }
}