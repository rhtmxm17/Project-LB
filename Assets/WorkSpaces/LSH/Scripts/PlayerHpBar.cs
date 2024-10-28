using Michsky.UI.Dark;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 플레이어 체력바와 색상에 관여하는 스크립트
/// </summary>
public class PlayerHpBar : MonoBehaviour
{

    PlayerModel player; //현재체력

    private Slider slider;
    [SerializeField] private TMP_Text curHp;  //체력바 숫자 표기
    [SerializeField] private float HpPercent; //체력바 퍼센트
    [SerializeField] private Image HpFillImg; //체력바 색상

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        player = GameManager.Instance.GetPlayerModel();
        player.OnHpChange += HpBarColor;
        HpBarColor();
    }



    

    protected void HpBarColor()
    {
        ViewGraph();

        //if (true/* "느려져 버프" 해당될 경우 */)
        //{
        //    Debug.Log(3);
        //    HpFillImg.color = Color.blue;
        //    //속도도 느려짐

        //}
        //else
        //{
            if (HpPercent < 0.4f)// 체력 40퍼 미만일때
            {
            Debug.Log(1);
                HpFillImg.color = Color.red;
                //속도도 느려짐

            }
            else//체력 40퍼 이상일때
            {
            Debug.Log(2);
                HpFillImg.color = Color.green;
                //속도 정상임
            }
        
        //}

        

    }

    protected void ViewGraph()
    {
        curHp.text = ($"{player.Hp} / {player.MaxHp}").ToString();
        HpPercent = ((float)player.Hp / player.MaxHp);
        slider.value = HpPercent;
    }

}
