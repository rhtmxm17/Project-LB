using UnityEngine;

public enum EquipSlot { SLOT1, SLOT2, SLOT3, SLOT4, SLOT5, SIZE }

public class HotKeySystem : MonoBehaviour
{
    [SerializeField]
    InventoryHotKeySlot[] hotSlots;

    /// <summary>
    /// 아이템 슬롯을 받아서 아이템을 장착
    /// </summary>
    /// <param name="itemSlot">장착할 아이템이 있는 아이템 슬롯</param>
    public void EquipItem(InventoryItemSlot itemSlot) 
    {

        if (!(itemSlot.Item is InventoryEquableItemSO))
        {
            Debug.LogError("장착할 수 없는 아이템 장착이 요청됨.");
            return;
        }

        //현재 장착하려는 아이템
        InventoryEquableItemSO curItem = (InventoryEquableItemSO)itemSlot.Item;
        int slotIdx = (int)curItem.slotType;

        if (slotIdx < 0 || slotIdx >= hotSlots.Length)
        {
            Debug.LogError("잘못된 핫 키 타입이 입력됨");
            return;
        }

        //이전에 장착되어있던 슬롯.
        InventoryInteractableItemSlot prv = hotSlots[slotIdx].PrvItemSlot;

        if (prv == null)
        {//이전에 장착했었던 슬롯(아이템)이 없는경우

            hotSlots[slotIdx].PrvItemSlot = (InventoryInteractableItemSlot)itemSlot;//장착중인 슬롯을 갱신하고.
            hotSlots[slotIdx].PrvItemSlot.SetImageUsing();//원본 슬롯에 사용중임을 표시한 뒤
            hotSlots[slotIdx].SetItem(curItem);//아이템 이미지를 설정.

        }
        else if (prv.Item == curItem)
        {//이전에 장착되어있던 슬롯의 아이템과 장착하려는 아이템이 같은 경우

            hotSlots[slotIdx].PrvItemSlot.SetImageDefault();//인벤토리 이미지를 활성화.

            hotSlots[slotIdx].SetItem(null);//핫키를 비우고.
            hotSlots[slotIdx].PrvItemSlot = null;//장착을 해제.
        }
        else
        {//다른 아이템이 장착되어있었던 경우 

            itemSlot.SetImageUsing();//장착할 슬롯은 비활성화하고.
            hotSlots[slotIdx].PrvItemSlot.SetImageDefault();//이전 아이템을 다시 활성화하고.
            hotSlots[slotIdx].PrvItemSlot = (InventoryInteractableItemSlot)itemSlot;//장착할 슬롯을 설정한 뒤.
            hotSlots[slotIdx].SetItem(curItem);//아이템 이미지를 설정.

        }

    }

}
