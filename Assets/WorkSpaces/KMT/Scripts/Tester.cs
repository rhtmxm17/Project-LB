using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField]
    InventoryItemSO itemSO;

    [SerializeField]
    int idx;

    [SerializeField]
    InventorySystem inven;

    public void AddButton()
    { 
        inven.AddItem(itemSO, idx);
    }

    public void RemoveButton()
    {
        inven.RemoveItem(idx);
    }
}
