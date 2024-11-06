using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Npc/StageConditionDialogue")]
public class StageConditionDialogueSO : ScriptableObject
{

    //todo : 스테이지 데이터 관리를 0, 1, 2, 3로 한다고 가정.
    public enum Stages { STAGE1, STAGE2, STAGE3, STAGE4 };

    [Serializable]
    public struct ConditionDialogues {

        [SerializeField]
        public Stages stage;
        [SerializeField]
        [field: TextArea(10,50)]
        public string text;

    }


    [field: SerializeField]
    public ConditionDialogues[] C_Dialogues { get; private set; }

}
