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

    /// <summary>
    /// "느려져 디버프" 테스트용 bool 변수입니다. 추후 삭제 예정입니다.
    /// </summary>
    [SerializeField] public bool isSlow;

    /// <summary>
    /// "느려져 디버프" 테스트용 Vector4 변수입니다. 추후 삭제 예정입니다.
    /// </summary>
    Vector4 hpColor;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        hpColor = new Vector4(HpFillImg.color.r, HpFillImg.color.g, HpFillImg.color.b, HpFillImg.color.a);
    }

    private void Start()
    {
        player = GameManager.Instance.GetPlayerModel();
        player.OnHpChange += ChangeHpBarColor;
        ChangeHpBarColor();
        isSlow = false;
        hpColor = HpFillImg.color; //원래색 저장  
    }


    /// <summary>
    /// 현재는 디버프에 대한 이벤트가 없어서, 색상변경 테스트를 위해 작성된 Update문입니다. 추후 삭제 예정입니다.
    /// </summary>
    private void Update()
    {
        ChangeHpBarColor();

    }



    protected void ChangeHpBarColor()
    {
        ViewGraph();

        if (isSlow)
        {
            HpFillImg.color = Color.blue;
            //속도도 느려짐
            //.

        }
        else
        {
            if (HpPercent < 0.4f)// 체력 40퍼 미만일때
            {
                HpFillImg.color = Color.red;
                //속도도 느려짐
                //.

            }
            else//체력 40퍼 이상일때
            {
                HpFillImg.color = Color.green;
                //속도 정상됨
                //.

            }

        }


    }

    protected void ViewGraph()
    {
        curHp.text = ($"{player.Hp} / {player.MaxHp}").ToString();
        HpPercent = ((float)player.Hp / player.MaxHp);
        slider.value = HpPercent;
    }


}

