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
        PlayerData data = GameManager.Instance.GetPlayerData();
        int[] clearArr = data.stageClearCntArr;
        bool isStageCleared = data.isStageCleared;


        bool clearExist = false;

        foreach (int i in clearArr)
        {
            if (clearArr[i] != 0) clearExist = true;
        }

        if (!clearExist)
        {
            foreach (ConditionDialogues dialogue in c_Dialogues.C_Dialogues)
            {
                if (dialogue.stage == 0)
                {
                    conditionText = dialogue.text;
                }
            }

            return;
        }

        if (!isStageCleared)
        {
            return;
        }

        for (int i = 0; i < clearArr.Length - 1; i++)
        {
            if (clearArr[i] == 1)
            {
                foreach (ConditionDialogues dialogue in c_Dialogues.C_Dialogues)
                {
                    if (dialogue.stage == (Stages)(i + 1))
                    {
                        conditionText = dialogue.text;
                        return;
                    }
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
