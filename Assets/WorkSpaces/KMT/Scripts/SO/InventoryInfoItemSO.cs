using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Inventory/Items/InfoItem")]
public class InventoryInfoItemSO : InventoryItemSO, IClickable
{

    //[SerializeField]
    //int uiobj or infoSO;
    //todo : 설명 데이터, ui구조 정의하기.

    public void OnClickEvent(PointerEventData eventData)
    {
        //todo : 설명 ui 띄우기.
        //todo : 이미 설명이 나와있으면 중복호출 막기(깜빡거리는 현상 방지)
    }

}
