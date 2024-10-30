using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Inventory/Items/InfoItem")]
public class InventoryInfoItemSO : InventoryItemSO
{

    [field: SerializeField]
    [field: TextArea]
    public string Description {  get; private set; }

    [field: SerializeField]
    public DetailDescriptionSO DetailInfoSO { get; private set; } = null;

}
