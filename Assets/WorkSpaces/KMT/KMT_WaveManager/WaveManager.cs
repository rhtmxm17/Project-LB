using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [SerializeField]
    GameObject[] walls;

    /// <summary>
    /// 트리거에 연결하여 웨이브가 시작함을 알림
    /// </summary>
    public void StartWaveTrigger()
    {
        foreach (GameObject wall in walls)
        {
            wall.SetActive(true);
        }
    }

    public void CheckWaveIsClear()
    {
        if (transform.childCount <= 0)
        {
            //todo : wave 종료
            foreach (GameObject wall in walls)
            {
                wall.SetActive(false);
            }
        }
    }

}
