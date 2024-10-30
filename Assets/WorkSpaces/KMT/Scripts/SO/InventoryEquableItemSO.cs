using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Inventory/Items/EquableItem")]
public class InventoryEquableItemSO : InventoryInfoItemSO
{
    //todo : 인터페이스 제거
    [field: Header("Equip Type")]
    [field: SerializeField]
    public EquipSlot equipQuickSlotType {  get; private set; }

    public bool IsEquip {  get; private set; }

    [field: Header("Param")]
    [field: SerializeField]
    public int AttackPoint { get; private set; }
    [field: SerializeField]
    public int AdditiveAttackPoint { get; private set; }

    [field: SerializeField]
    public int MaxUpgradeLevel { get; private set; }
}
