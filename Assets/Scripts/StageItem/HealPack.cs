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

    [SerializeField, Tooltip("습득시 늘어나는 사용 가능 횟수")] int amount = 1;

    // 플레이어에게 영향줄 수 있도록, 플레이어와 연결
    StagePlayerControl player;
    //// 체력 상승수치를 직렬화
    //[SerializeField] private int healValue;

    

    private void Start()
    {
        if (!GameManager.Instance.GetPlayerModel().TryGetComponent(out player))
        {
            Debug.LogWarning("스테이지에 유효한 플레이어가 없거나 스테이지가 아닌 곳에 스테이지용 습득 아이템이 배치됨");
            return;
        }
    }

    protected override void Pickup()
    {
        base.Pickup();

        Debug.Log($"회복 아이템 {amount}개 습득");

        player.HealthPackUsage += amount;
    }
}
