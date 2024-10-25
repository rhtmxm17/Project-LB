using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 오브젝트의 최하위 클래스 힐팩 아이템입니다.
/// 충돌 시 장착되어, 사용시 플레이어의 체력을 올려줍니다.
/// </summary>
public class HealPack : Collection
{

    // 아이디
    [SerializeField] private int itemID;


    // 플레이어에게 영향줄 수 있도록, 플레이어와 연결
    [SerializeField] private PlayerModel player;
    // 체력 상승수치를 직렬화
    [SerializeField] private int healValue;

    

    private void Start()
    {
        player = FindAnyObjectByType<PlayerModel>();

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PickupSound();

            Heal();

        }

    }


    private void UseItem()
    {
        
        player.Hp += healValue;
        
    }



}
