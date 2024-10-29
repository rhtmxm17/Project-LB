using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipSlot { SLOT1, SLOT2, SLOT3, SLOT4, SLOT5, SIZE }

public class HotKeySystem : MonoBehaviour
{
    [SerializeField]
    InventoryItemSlot[] hotSlots;

    /// <summary>
    /// 아이템SO를 전달받아 대응되는 핫키슬롯에 장착.
    /// [슬롯 범위가 잘못된경우 에러 출력]
    /// </summary>
    /// <param name="item">장착할 아이템</param>
    /// <returns>이전에 장착되어있던 아이템SO, 아이템이 없었다면 null반환.</returns>
    public InventoryItemSO SetItem(InventoryEquableItemSO item) 
    {
        int slotIdx = (int)item.slotType;

        if (slotIdx < 0 || slotIdx >= hotSlots.Length)
        {
            Debug.LogError("잘못된 핫 키 타입이 입력됨");
            return null;
        }

        InventoryItemSO prv = hotSlots[slotIdx].Item;

        //장착 취소
        if (prv == item)
        {
            hotSlots[slotIdx].SetItem();
        }
        else//장착 교체
        {
            hotSlots[slotIdx].SetItem(item);
        }

        //같은거면 본인 반환, 다른거면 이전것 반환, 없었다면 null반환.
        return prv;

    }

}
