using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField]
    Transform itemSlotParent;

    [SerializeField]
    int slotCount;

    [SerializeField]
    GameObject ItemSlotPrefab;

    InventoryItemSlot[] itemSlotArr;

    private void Awake()
    {
        itemSlotArr = new InventoryItemSlot[slotCount];

        for (int i = 0; i < slotCount; i++)
        { 
            itemSlotArr[i] = 
                Instantiate(ItemSlotPrefab, itemSlotParent)
                .GetComponentInChildren<InventoryItemSlot>();

        }

    }


    //today todo : 겹쳐지는 아이템인 경우, 갯수를 더하도록 구현

    /// <summary>
    /// 아이템을 슬롯에 저장
    /// </summary>
    /// <param name="item"></param>
    /// <param name="idx"></param>
    public void AddItem(InventoryItemSO item, int idx)
    {
        if (idx < 0 || idx >= itemSlotArr.Length)
        {
            Debug.LogError("아이템 창 범위를 벗어난 인덱스 값");
            return;
        }

        itemSlotArr[idx].SetItem(item);

    }


    public void RemoveItem(int idx)
    {
        if (idx < 0 || idx >= itemSlotArr.Length)
        {
            Debug.LogError("아이템 창 범위를 벗어난 인덱스 값");
            return;
        }

        itemSlotArr[idx].SetItem();

    }

}
