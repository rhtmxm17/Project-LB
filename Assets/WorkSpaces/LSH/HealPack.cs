using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 오브젝트의 최하위 클래스 힐팩 아이템입니다.
/// 충돌 시, 소모되어 플레이어의 체력을 올려줍니다.
/// </summary>
public class HealPack : Consumption
{

    private void OnTriggerEnter(Collider other)
    {        
        if (other.tag == "Player")
        {
            Pickup("힐팩(체력회복)");
        }

    }

    

}
