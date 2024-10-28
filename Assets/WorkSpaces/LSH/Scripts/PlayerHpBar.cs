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


    PlayerModel player;

    float playerHpPercent;

    Image playerHpImg;

    private void Awake()
    {

    }

    private void Start()
    {
        player.OnHpChange += ViewHp;
    }



    protected void ViewHp()
    {
        playerHpPercent = (player.Hp / player.Hp);
        player.OnHpChange -= ViewHp;
    }


}
