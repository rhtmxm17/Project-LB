using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PanelMainSetting : MonoBehaviour
{

    [SerializeField] Slider slider1MouseSensitive;
    [SerializeField] Slider slider2BGM;
    [SerializeField] Slider slider3Voice;
    [SerializeField] Slider slider4Effect;
    [SerializeField] TMP_Text num1Value;
    [SerializeField] TMP_Text num2Value;
    [SerializeField] TMP_Text num3Value;
    [SerializeField] TMP_Text num4Value;


    private void Start()
    {
                
        if (GameManager.Instance.GetPlayerData().MouseSensitive < 0)
        {
            slider1MouseSensitive.value = 50;
        }
        else
        {
            slider1MouseSensitive.value = GameManager.Instance.GetPlayerData().MouseSensitive;

        }
        
        ChangeValue();

    }

    public void ChangeValue()
    {
        num1Value.text = ((int)slider1MouseSensitive.value).ToString();
        num2Value.text = ((int)slider2BGM.value).ToString();
        num3Value.text = ((int)slider3Voice.value).ToString();
        num4Value.text = ((int)slider4Effect.value).ToString();

    }

    public void Submit()
    {
        int mouseSensitive = (int)slider1MouseSensitive.value;
        GameManager.Instance.GetPlayerData().MouseSensitive = mouseSensitive;


    }


}
