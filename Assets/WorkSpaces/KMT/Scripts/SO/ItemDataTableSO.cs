using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/DataTable/DataTable")]
public class ItemDataTableSO : ScriptableObject
{

    public Dictionary<ItemType, InventoryItemSO> itemTable;

    [SerializeField]
    InventoryItemSO[] itemDatas;

    public void InitDataTable()
    {
        itemTable = new Dictionary<ItemType, InventoryItemSO>(itemDatas.Length * 2);

        foreach (InventoryItemSO data in itemDatas)
        {
            itemTable.Add(data.ItemType, data);
        }
    }

    /// <summary>
    /// 데이터테이블에서 아이템 정보를 가져옴
    /// </summary>
    /// <param name="type">가져올 아이템의 타입(ID)</param>
    /// <returns>찾을 수 없는 경우 null 반환</returns>
    public InventoryItemSO GetItemDataSO(ItemType type)
    {
        if (itemTable.ContainsKey(type))
        { 
            return itemTable[type];
        }
        else
        {
            return null;
        }

    }

     

}
