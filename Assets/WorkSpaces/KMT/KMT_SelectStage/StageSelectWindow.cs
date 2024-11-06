using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectWindow : MonoBehaviour
{

    [SerializeField]
    GameObject mainWindow;
    [SerializeField]
    GameObject saveWindow;
    [SerializeField]
    GameObject selectConformWindow; 
    [SerializeField]
    GameObject quitWindow;

    [SerializeField]
    Image stage4Img;
    [SerializeField]
    Button stage4Btn;

    [SerializeField]
    PlayerCharacterControllerControl playerCharacterControllerControl;

    int[] stageClearArr;

    private void Awake()
    {
        PlayerData playerData = GameManager.Instance.GetPlayerData();
        stageClearArr = playerData.stageClearCntArr;
    }

    private void Start()
    {
        if (stageClearArr[0] > 0 &&
            stageClearArr[1] > 0 &&
            stageClearArr[2] > 0)
        {
            stage4Img.color = new Color(1, 1, 1, 1);
            stage4Btn.gameObject.SetActive(true);
        }
        else
        {
            stage4Img.color = new Color(1, 1, 1, 0.13f);
            stage4Btn.gameObject.SetActive(false);
        }
    }

    public void OpenWindow()
    {
        gameObject.SetActive(true);
        playerCharacterControllerControl.MouseLock(false);
    }

    public void CloseWindow()
    {
        saveWindow.SetActive(false);
        selectConformWindow.SetActive(false);
        quitWindow.SetActive(false);

        gameObject.SetActive(false);
        playerCharacterControllerControl.MouseLock(true);

    }

}
