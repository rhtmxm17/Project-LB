using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Items/Item")]
public class InventoryItemSO : ScriptableObject
{

    [field: Header("UI")]
    [field: SerializeField]
    public Sprite ImgSprite {  get; set; }
    [field: SerializeField]
    public string ItemName { get; private set; }

    [Header("Max Count")]
    [SerializeField]
    int maxCnt;

    public int ItemCount {  get; private set; }

}
