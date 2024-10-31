using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SlotSetter : MonoBehaviour
{
    [SerializeField]
    InventoryItemSlot itemSlot;

    [SerializeField]
    InventoryItemSO inventoryItemSO;

    [SerializeField]
    UpgradeSystem upgradeSystem;

    private void Awake()
    {
        Button btn = GetComponent<Button>();
        btn.onClick.AddListener(SetItemSlot);
    }

    public void SetItemSlot()
    { 
        itemSlot.SetItem(inventoryItemSO);
        upgradeSystem.SetFocusedItem(inventoryItemSO);
    }
}
