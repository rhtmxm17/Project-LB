using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasMainScene : MonoBehaviour
{
    [SerializeField] GameObject panelMainLogo;
    [SerializeField] GameObject panelMainGuide;
    [SerializeField] GameObject panelMainSetting;
    [SerializeField] GameObject panelMainExit;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        MainLogo();
    }

    /// <summary>
    /// Play버튼 눌렀을때 실행될 함수. 벙커씬으로 이동됨
    /// </summary>
    public void GamePlay()
    {
        GameManager.Instance.GetSceneChanger().ChangeScene(SceneChanger.Scenes.BUNCKER);
    }

    /// <summary>
    /// Exit버튼 눌렀을때 실행될 함수. 유니티 게임창이 종료됨
    /// </summary>
    public void GameExit()
    {
        Application.Quit();
    }

    public void MainLogo()
    {
        panelMainLogo.SetActive(true);
        panelMainGuide.SetActive(false);
        panelMainSetting.SetActive(false);
        panelMainExit.SetActive(false);
    }

    public void MainGuide()
    {
        panelMainLogo.SetActive(false);
        panelMainGuide.SetActive(true);
        panelMainSetting.SetActive(false);
        panelMainExit.SetActive(false);
    }


    public void MainSetting()
    {
        panelMainLogo.SetActive(false);
        panelMainGuide.SetActive(false);
        panelMainSetting.SetActive(true);
        panelMainExit.SetActive(false);
    }


    public void MainExit()
    {
        panelMainLogo.SetActive(false);
        panelMainGuide.SetActive(false);
        panelMainSetting.SetActive(false);
        panelMainExit.SetActive(true);
    }




}
