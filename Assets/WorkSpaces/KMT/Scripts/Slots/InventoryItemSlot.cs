using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryItemSlot : MonoBehaviour
{
    //todo : 테스트용
    public InventoryItemSO Item;
    protected Image img;

    protected virtual void Awake()
    {
        img = GetComponent<Image>();

        if (Item != null)
        {
            img.sprite = Item.ImgSprite;
        }


    }

    /// <summary>
    /// 슬롯이 차지할 아이템을 바꿈.
    /// null인 경우(기본) 아이템 창을 비움.
    /// </summary>
    /// <param name="item"></param>
    public void SetItem(InventoryItemSO item = null)
    {
        if (item == null)
        {
            img.sprite = null;
        }
        else
        {
            img.sprite = item.ImgSprite;
        }

        Item = item;
    }

    public void SetImageDefault()
    {
        img.color = InventoryItemSO.ORIGIN_COLOR;
    }

    public void SetImageUsing()
    {
        img.color = InventoryItemSO.USING_COLOR;

    }

}
