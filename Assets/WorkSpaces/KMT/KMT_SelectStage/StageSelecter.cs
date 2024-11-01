using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class StageSelecter : MonoBehaviour
{

    [SerializeField]
    StageData stageData;

    [SerializeField]
    StageConformWindow conformWindow;

    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClicked);
    }

    void OnClicked()
    {
        if (stageData == null)
        {
            Debug.LogError("스테이지 정보 데이터가 없음");
            return;
        }

        conformWindow.OpenWindow(stageData);

    }

}
