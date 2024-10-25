using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 오브젝트의 최하위 클래스 일지 아이템입니다.
/// 충돌 시, 일지가 수집됩니다.
/// </summary>
public class Diary : Collection
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Pickup("일지");
        }

    }



}