using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 오브젝트의 최하위 클래스 식량 아이템입니다.
/// 충돌 시, 소모되어 플레이어의 경험치를 올려줍니다.
/// </summary>
public class Food : Consumption
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Pickup("식량(경험치업)");
        }

    }



}