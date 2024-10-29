using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Inventory/Items/EquableItem")]
public class InventoryEquableItemSO : InventoryInfoItemSO, IDoubleClickable
{
    public void OnDoubleClickEvent(PointerEventData eventData)
    {
        //todo : 무기 타입에 따라서 장★착 하기.
    }


}
