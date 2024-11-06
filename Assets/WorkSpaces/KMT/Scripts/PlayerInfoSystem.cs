using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInfoSystem : MonoBehaviour
{

    PlayerData playerData = null;

    const string MAX = "MAX / MAX";

    [SerializeField]
    TextMeshProUGUI hp;
    [SerializeField]
    TextMeshProUGUI level;
    [SerializeField]
    TextMeshProUGUI exp;

    [SerializeField]
    PlayerCharacterControllerControl playerCharacterControllerControl;


    private void Awake()
    {
        playerData = GameManager.Instance.GetPlayerData();
    }

    private void Start()
    {

        hp.text = playerData.MaxHP.ToString();
        level.text = playerData.Level.ToString();

        if (playerData.Level >= playerData.ReqExpArr.Length)
        {
            exp.text = MAX;
        }
        else
        { 
            exp.text = $"{playerData.Exp} / {playerData.ReqExpArr[playerData.Level]}";
        }

        CloseWindow();

    }

    public void OpenWindow()
    {
        gameObject.SetActive(true);
        playerCharacterControllerControl.MouseLock(false);
    }

    public void CloseWindow()
    {
        gameObject.SetActive(false);
        playerCharacterControllerControl.MouseLock(true);
    }

}
