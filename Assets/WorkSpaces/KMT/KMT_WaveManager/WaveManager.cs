using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{

    [SerializeField]
    GameObject[] walls;

    [SerializeField]
    Rigidbody rg = null;

    private void Start()
    {
        foreach (GameObject wall in walls)
        { 
            wall.SetActive(false);
        }
    }

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
            Debug.LogWarning("웨이브 종료");

            //todo : wave 종료
            foreach (GameObject wall in walls)
            {
                wall.SetActive(false);
            }

            if (rg != null)
            { 
                rg.useGravity = true;
            }
        }
    }

}
