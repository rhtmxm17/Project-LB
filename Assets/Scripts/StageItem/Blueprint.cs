using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 오브젝트의 최하위 클래스 설계도 아이템입니다.
/// 충돌 시, 설계도가 수집됩니다.
/// </summary>
public class Blueprint : Collection
{

    // 아이디
    [SerializeField] private int itemID;



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PickupSound();

            PaPer();
        }

    }


}

