using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static StageConditionDialogueSO;

[Serializable]
public class PlayerData
{

    public int level;
    public float exp;
    public int[] stageClearCntArr;
    public bool[] hiddenArr;
    public bool[] storyArr;

    public int food;
    public int gear;

}
