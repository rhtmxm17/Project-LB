using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StageSaveLoad : MonoBehaviour
{
    [SerializeField] TMP_Text firstLoad;    ///데이터를 처음 세이브할때 출력될 문구
    [SerializeField] TMP_Text WantLoad;     ///데이터를 세이브 한 적이 있을때 출력될 문구

    /// <summary>
    /// 데이터 덮어쓰는 함수입니다. 작성이 필요합니다. 
    /// </summary>
    public void ButtonOverWrite()
    {
        //FIXME: 데이터 덮어쓰기 부탁드립니다
        Debug.LogWarning("FIXME: 데이터 덮어쓰기 부탁드립니다");
    }

    /// <summary>
    /// 데이터 불러오는 함수입니다. 작성이 필요합니다. 
    /// </summary>
    public void ButtonLoad()
    {
        //FIXME: 데이터 불러오기 부탁드립니다
        Debug.LogWarning("FIXME: 데이터 불러오기 부탁드립니다");
    }

}
