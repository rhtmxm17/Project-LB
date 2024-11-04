using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// UI 프리팹 내부의 필요한 참조를 전달하는 클래스
/// </summary>
public class StageClearUI : MonoBehaviour
{
    [SerializeField] TMP_Text foodGainValueText;
    [SerializeField] TMP_Text gearGainValueText;

    /// <summary>
    /// 클리어 UI에 표시될 식량 확득량 텍스트 설정
    /// </summary>
    /// <param name="text">설정할 텍스트</param>
    public void SetFoodGain(string text) => foodGainValueText.SetText(text);

    /// <summary>
    /// 클리어 UI에 표시될 부품 확득량 텍스트 설정
    /// </summary>
    /// <param name="text">설정할 텍스트</param>
    public void SetGearGain(string text) => gearGainValueText.SetText(text);

    private void Start()
    {
        GameManager.Instance.GetStageSceneManager().InitClearUI(this);
    }
}
