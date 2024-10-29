using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryItemSlot : MonoBehaviour
{
    //todo : 테스트용
    public InventoryItemSO Item;
    /*    public InventoryItemSO Item 
        { 
            get { return Item; }
            set 
            { 
                Item = value;
                //if(Item != null)
                //img = Item.img;
                //todo : 스프라이트 타입 이미지.
            } 
        }*/

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

}
