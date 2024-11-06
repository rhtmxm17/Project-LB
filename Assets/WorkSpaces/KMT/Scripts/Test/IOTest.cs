using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class IOTest : MonoBehaviour
{
    [SerializeField]
    PlayerData d = null;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("saved");
            FileIOSystem.SavePlayerData(GameManager.Instance.GetPlayerData());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("idx : " +
            GameManager.Instance.GetPlayerData().inventoryData[3].invenIdx);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerData data = FileIOSystem.LoadPlayerData();
            if (data == null)
            {
                Debug.LogWarning("불러올 데이터가 없습니다. 새로 만듭니다.");
                data = new PlayerData(GameManager.Instance.GetItemDataTable());
            }

            d = data;

        }

    }
}
