using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Inventory/Items/EquableItem")]
public class InventoryEquableItemSO : InventoryInfoItemSO, IDoubleClickable
{
    //todo : 인터페이스 제거
    [field: Header("Equip Type")]
    [field: SerializeField]
    public EquipSlot slotType {  get; private set; }

    public bool IsEquip {  get; private set; }

    public void OnDoubleClickEvent(PointerEventData eventData)
    {
        //todo : 무기 타입에 따라서 장★착 하기.
    }


}
