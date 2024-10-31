using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static StageConditionDialogueSO;
using static Unity.Burst.Intrinsics.X86.Avx;

public class kmt_ConditionInteractableNpc : InteractableNpc
{

    [SerializeField]
    StageConditionDialogueSO c_Dialogues;

    string conditionText = null;

    protected override void Awake()
    {

        base.Awake();
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

    protected override void ShowDialogue()
    {

        if (conditionText != null)
        {
            tmp.text = conditionText;
            animator.Play("FadeIn");
            conditionText = null;
        }
        else
        {
            base.ShowDialogue();
        }
    }

}
