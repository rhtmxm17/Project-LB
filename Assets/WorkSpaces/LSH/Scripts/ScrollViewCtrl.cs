using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewCtrl : MonoBehaviour
{

    [SerializeField] GameObject stageList;
    [SerializeField] GameObject saveLoad;


    private void Start()
    {
        OpenStageList();
    }


    public void OpenStageList()
    {
        stageList.SetActive(true);
        saveLoad.SetActive(false);
    }

    public void OpenSaveLoad()
    {
        stageList.SetActive(false);
        saveLoad.SetActive(true);
    }

}
