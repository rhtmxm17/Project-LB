using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 아이템 스크립트의 최상위 클래스입니다.
/// 하위 클래스는 Consumption(소모)과 Collection(수집)이 있습니다.
/// </summary>
public abstract class Item : MonoBehaviour
{


    protected virtual void Pickup(string name) 
    {
        //획득소리출력
        Debug.Log($"아이템:{name} 획득 SF 출력");

        //사라지기
        this.gameObject.SetActive(false);
    }

    

}
