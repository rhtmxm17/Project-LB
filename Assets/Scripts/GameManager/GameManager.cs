using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SceneChanger))]
public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    PlayerData playerData = null;
    SceneChanger sceneChanger = null;

    PlayerModel playerModel = null;
    

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerData = FileIOSystem.LoadPlayerData();
            sceneChanger = GetComponent<SceneChanger>();

            SceneManager.sceneLoaded += OnSceneLoaded;

            if (playerData == null)
                playerData = new PlayerData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Resources 폴더에서 불러옴. / Resources/Singletons/Manager 프리팹 가져옴
    public static void Create() 
    {
        Instantiate(Resources.Load<GameManager>("Singletons/GameManager"));
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) 
    {
        playerModel = null;
        playerModel = GameObject.FindWithTag("Player").GetComponent<PlayerModel>();
    }

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

    public SceneChanger GetSceneChanger()
    {
        return sceneChanger;
    }

    /// <summary>
    /// 플레이어의 모델 컴포넌트를 가져옴
    /// </summary>
    /// <returns>플레이어가 없을경우 null 반환</returns>
    public PlayerModel GetPlayerModel() 
    {
        return playerModel;
    }

}
