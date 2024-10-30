using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoPanel : MonoBehaviour
{

    const string clear = "";

    [SerializeField]
    TextMeshProUGUI TitleTMP;

    [SerializeField]
    TextMeshProUGUI DescriptionTMP;

    [SerializeField]
    TextMeshProUGUI AttackTMP;

    /// <summary>
    /// 설명창의 모든 텍스트를 지움
    /// </summary>
    public void ClearAllText()
    {
        TitleTMP.text = clear;
        DescriptionTMP.text = clear;
        AttackTMP.text = clear;
    }

    /// <summary>
    /// 아이템 설명의 이름, 강화수치를 설정
    /// [이름 + 강화수치]
    /// </summary>
    /// <param name="str">아이템의 이름</param>
    /// <param name="addNum">강화수치가 있을 경우 강화수치 입력</param>
    public void SetTitleText(in string str, int addNum = 0)
    {

        if (addNum != 0)
        {
            TitleTMP.text = $"{str} + {addNum}";
        }
        else
        {
            TitleTMP.text = str;
        }

    }

    /// <summary>
    /// 아이템 설명의 상세 설명 텍스트를 설정
    /// </summary>
    /// <param name="str">아이템 세부 설명 문자열</param>
    public void SetDescriptionText(in string str)
    {
        DescriptionTMP.text = str;
    }

    /// <summary>
    /// 아이템 설명 하단의 공격수치 텍스트를 설정
    /// [공격력 attack + upgradedAttack]
    /// </summary>
    /// <param name="attack">공격력으로 출력할 수치</param>
    /// <param name="upgradedAttack">추가 공격력으로 출력할 수치</param>
    public void SetLowerText(int attack, int upgradedAttack = 0)
    {
        if (upgradedAttack != 0)
        {
            AttackTMP.text = $"공격력 {attack} + {upgradedAttack}";
        }
        else
        {
            AttackTMP.text = $"공격력 {attack}";
        }
    }

}
