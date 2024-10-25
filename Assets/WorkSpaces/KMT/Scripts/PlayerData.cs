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

        //todo : 디버그용 코드. 후에 제거
        //today todo : 데이터 구조 정립, 관리 데이터 인스턴스 싱글톤으로 설계
        Debug.Log(level);
        Debug.Log(exp);

        foreach (var item in isClearArr) { 
            Debug.Log(item);
        }

    }
}
