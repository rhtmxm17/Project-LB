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

    private Slider slider;
    [SerializeField] private int HpPercent; //체력바 퍼센트
    [SerializeField] private Image HpFillImg; //체력바 색상

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void Start()
    {
        player = GameManager.Instance.GetPlayerModel();
        HpBarColor();
        player.OnHpChange += HpBarColor;
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
            if (HpPercent >= 40.0f) //체력 40퍼 이상일때
            {
                HpFillImg.color = Color.green;
                //속도 정상임
            }
            else if(HpPercent < 40.0f)// 체력 40퍼 미만일때
            {
                HpFillImg.color = Color.red;
                //속도도 느려짐

            }
        //}

        

    }

    protected void ViewGraph()
    {

        HpPercent = player.Hp/*이후 모델쪽에 최대체력 생기면 Hp를 maxHp로 바꿔주기 or MaxValue를 건드리기*/;
        slider.value = HpPercent;
    }

}
