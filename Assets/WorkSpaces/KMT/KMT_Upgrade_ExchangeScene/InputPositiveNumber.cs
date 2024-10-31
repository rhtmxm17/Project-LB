using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_InputField))]
public class InputPositiveNumber : MonoBehaviour
{

    TMP_InputField inputField;

    [SerializeField]
    TextMeshProUGUI[] curTexts;

    const string empty = "";
    const string zero = "0";
    const string full = "99";

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onValueChanged.AddListener(CheckPositiveNumver);
    }

    void CheckPositiveNumver(string str)
    {

        if (!int.TryParse(str, out int result) || result < 0 || result >= 100)
        {

            if (result >= 100)
            {
                inputField.text = full;
                SetTexts(full);
            }
            else
            {
                inputField.text = empty;
                SetTexts(empty);
            }

        }
        else if (result == 0 || str.StartsWith(zero))
        {
            inputField.text = zero;
            SetTexts(zero);
        }
        else
        {
            SetTexts(str);
        }
    }

    void SetTexts(in string str)
    {
        foreach (var text in curTexts)
        {
            text.text = str;
        }
    }

    /// <summary>
    /// 입력되어있는 양수값을 가져옴(0포함)
    /// </summary>
    /// <returns>반환할 수 없는 경우 -1 반환</returns>
    public int GetValue()
    {
        string str = inputField.text;

        if (int.TryParse(str, out int result) || result >= 0)
        {
            return result;
        }
        else
        {
            return -1;
        }
    }

    public void InitField()
    {
        inputField.text = empty;
        SetTexts(empty);
    }

}
