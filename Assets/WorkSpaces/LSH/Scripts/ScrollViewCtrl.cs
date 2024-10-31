using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollViewCtrl : MonoBehaviour
{

    [SerializeField] GameObject stageList;
    [SerializeField] GameObject saveLoad;


    private void Start()
    {
        stageList.SetActive(false);
    }


    public void StageListOpen()
    {
        stageList.SetActive(true);
    }

    public void SaveLoadOpen()
    {
        saveLoad.SetActive(true);
    }

}
