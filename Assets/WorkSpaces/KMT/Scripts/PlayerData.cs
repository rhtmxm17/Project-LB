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
    public int[] isClearArr;

    public void PrintAll() {

        Debug.Log(level);
        Debug.Log(exp);

        foreach (var item in isClearArr) { 
            Debug.Log(item);
        }

    }
}
