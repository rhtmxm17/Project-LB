using Michsky.UI.Dark;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어 체력바와 색상에 관여하는 스크립트
/// </summary>
public class PlayerHpBar : MonoBehaviour
{

    PlayerModel player; //현재체력

    float playerHpPercent; //퍼센트
    Image playerHpImg; //이미지

    private void Awake()
    {
        
    }

    private void Start()
    {
        player = GameManager.Instance.GetPlayerModel();
        player.OnHpChange += ViewHp;
    }



    protected void ViewHp()
    {

        playerHpPercent = (player.Hp / player.Hp/*이후에 모델쪽 최대체력 생기면 maxHp로 바꿔주기*/);
        
    }


}
