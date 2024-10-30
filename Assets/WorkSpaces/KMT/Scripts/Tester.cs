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


    [SerializeField]
    int foodAmount;

    public void AddButton()
    { 
        inven.AddItem(itemSO, idx);
    }

    public void RemoveButton()
    {
        inven.RemoveItem(idx);
    }

    public void AddFood()
    { 
        inven.AddFood(foodAmount);
    }

    public void UseFood()
    { 
        inven.UseFood(foodAmount);
    }
}
