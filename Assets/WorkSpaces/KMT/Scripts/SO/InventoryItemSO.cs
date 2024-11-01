using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Items/Item")]
public class InventoryItemSO : ScriptableObject
{

    [field: Header("ItemType")]
    [field: SerializeField]
    public ItemType ItemType {  get; private set; }

    [field: Header("Inventory Index")]
    [field: SerializeField]
    public int InventoryIdx { get; private set; }

    [field: Header("UI")]
    [field: SerializeField]
    public Sprite ImgSprite {  get; private set; }
    [field: SerializeField]
    public string ItemName { get; private set; }

    [field: Header("Max Count")]
    [field: SerializeField]
    public int MaxCnt { get; private set; } = 1;

    public static readonly Color ORIGIN_COLOR = Color.white;
    public static readonly Color USING_COLOR = new Color(1, 1, 1, 0.5f);

}
