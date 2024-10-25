using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{

    PlayerData playerData;
    private void Awake()
    {
        playerData = new PlayerData();
    }

    public int i;
    public float f;
    public int[] arrs;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            playerData.level = i;
            playerData.exp = f;
            playerData.isClearArr = arrs;

            FileIOSystem.SavePlayerData(playerData);
        }

        if (Input.GetKeyDown(KeyCode.Q)) {

            PlayerData tmpD = FileIOSystem.LoadPlayerData();
            tmpD.PrintAll();
        }
    }
}
