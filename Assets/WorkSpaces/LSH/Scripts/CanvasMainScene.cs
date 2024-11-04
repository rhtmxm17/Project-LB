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
        MainLogo();
    }

    /// <summary>
    /// Play버튼 눌렀을때 실행될 함수. 벙커씬으로 이동됨
    /// </summary>
    public void GamePlay()
    {
        Debug.Log("벙커씬 연결 부탁드립니다");
    }

    /// <summary>
    /// Exit버튼 눌렀을때 실행될 함수. 유니티 게임창이 종료됨
    /// </summary>
    public void GameExit()
    {
        Environment.Exit(0);
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
        Debug.Log("세팅 패널 편집중입니다.");
        //panelMainLogo.SetActive(false);
        //panelMainGuide.SetActive(false);
        //panelMainSetting.SetActive(true);
        //panelMainExit.SetActive(false);
    }


    public void MainExit()
    {
        panelMainLogo.SetActive(false);
        panelMainGuide.SetActive(false);
        panelMainSetting.SetActive(false);
        panelMainExit.SetActive(true);
    }




}
