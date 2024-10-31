using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class StageConformWindow : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI text;

    //todo : 싱글톤 가정
    StageSceneManager stageSceneManager;

    StageData curData = null;

    private void Awake()
    {
        //stageSceneManager = GameManager.Instance.GetStageSceneManager();
    }

    public void OpenWindow(StageData stageData)
    {
        gameObject.SetActive(true);
        curData = stageData;
        //todo : 이름으로 받아오기.
        text.text = $"ARE YOU SURE YOU WANT TO PLAY {curData.ToString()}? ";
    }

    public void ConformButton()
    {
        stageSceneManager.StageDataTable = curData;
        stageSceneManager.EnterStage();
    }

}
