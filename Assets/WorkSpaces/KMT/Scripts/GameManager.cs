using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance { get; private set; }

    PlayerData playerData = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerData = FileIOSystem.LoadPlayerData();

            if (playerData == null)
                playerData = new PlayerData();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public PlayerData GetPlayerData()
    {
        return playerData;
    }

}
