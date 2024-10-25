using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StageConditionDialogueSO;

public class kmt_ConditionInteractableNpc : InteractableNpc
{

    [SerializeField]
    StageConditionDialogueSO c_Dialogues;

    string conditionText = null;

    private void Awake()
    {
        //GameData data = DataManager.GetData();
        //int lastCleared = data.lastClearedStage;
        //int[] clearArr = data.clearedArr;

        int[] clearArr = new int[4] { 1, 0, 0, 0 };
        int lastCleared = 0;

        if (lastCleared < 0 || lastCleared >= clearArr.Length) return;

        //todo : 스테이지는 0, 1, 2, 3으로 관리한다고 가정.
        if (clearArr[lastCleared] == 1)
        {
            foreach (ConditionDialogues dialogue in c_Dialogues.C_Dialogues)
            {
                if (dialogue.stage == (Stages)lastCleared) {
                    conditionText = dialogue.text;
                    return;
                }
            }
        }

        conditionText = null;

    }


    public override void OnInteracted() {

        string text;

        if (conditionText != null)
        {
            //1회성으로 출력할 조건부 대사
            text = conditionText;
            conditionText = null;
        }
        else
        {
            //출력한 대사
            text = GetNormalText();
        }

        Debug.Log(text);

        //todo : 창 띄우기. => 대사를 매개변수로 전달하기? || 내가 ui객체를 가지고있기?
        OnInteractedEvent?.Invoke();

    }

}
