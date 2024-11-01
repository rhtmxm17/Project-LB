using System;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneChanger))]
public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    PlayerData playerData = null;
    SceneChanger sceneChanger = null;
    StageSceneManager stageSceneManager = null;

    PlayerModel playerModel = null;

    [SerializeField]
    ItemDataTableSO dataTable;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerData = FileIOSystem.LoadPlayerData();
            sceneChanger = GetComponent<SceneChanger>();
            stageSceneManager = GetComponent<StageSceneManager>();


            dataTable.InitDataTable();

            SceneManager.sceneLoaded += OnSceneLoaded;

            CursorOffSetting();

            if (playerData == null)
                playerData = new PlayerData(dataTable);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// LSH: 커서 Hide하고, 커서 영역을 센터로 잠급니다. 상점/강화 제외한 모든 인게임 전용 마우스 설정입니다.
    /// </summary>
    public void CursorOffSetting()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    /// <summary>
    /// LSH: 커서 Hide 풀고, 커서 영역을 외곽으로 풀어줍니다. 상점/강화 전용 마우스 설정입니다.
    /// </summary>
    public void CursorOnSetting()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }



    public void ReLoadData()
    {
        playerData = FileIOSystem.LoadPlayerData();
        if (playerData == null)
            playerData = new PlayerData(dataTable);

    }


    //Resources 폴더에서 불러옴. / Resources/Singletons/Manager 프리팹 가져옴
    public static void Create() 
    {
        Instantiate(Resources.Load<GameManager>("Singletons/GameManager"));
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        if (mode == LoadSceneMode.Single)
            playerModel = null;

        playerModel ??= GameObject.FindWithTag("Player")?.GetComponent<PlayerModel>();
    }

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public SceneChanger GetSceneChanger()
    {
        return sceneChanger;
    }

    public StageSceneManager GetStageSceneManager() => stageSceneManager;

    /// <summary>
    /// 플레이어의 모델 컴포넌트를 가져옴
    /// </summary>
    /// <returns>플레이어가 없을경우 null 반환</returns>
    public PlayerModel GetPlayerModel() 
    {
        return playerModel;
    }

    /// <summary>
    /// 데이터 테이블을 반환
    /// </summary>
    /// <returns></returns>
    public ItemDataTableSO GetItemDataTable()
    {
        return dataTable;
    }

}
