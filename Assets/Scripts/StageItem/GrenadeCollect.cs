using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeCollect : Collection
{
    [SerializeField, Tooltip("습득시 늘어나는 사용 가능 횟수")] int amount = 1;

    StagePlayerControl player;

    private void Start()
    {
        if (! GameManager.Instance.GetPlayerModel().TryGetComponent(out player))
        {
            Debug.LogWarning("스테이지에 유효한 플레이어가 없거나 스테이지가 아닌 곳에 스테이지용 습득 아이템이 배치됨");
            return;
        }
    }

    protected override void Pickup()
    {
        base.Pickup();

        Debug.Log($"수류탄 {amount}개 습득");

        player.GrenadeUsage += amount;
    }
}
