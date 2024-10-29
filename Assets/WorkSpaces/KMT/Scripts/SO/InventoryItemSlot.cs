using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class InventoryItemSlot : ClickCallback
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

    Image img;

    protected override void Awake()
    {
        base.Awake();

        ClickEvent += OnClickEvent;
        DoubleClickEvent += OnDoubleClickEvent;
        DragEvent += OnDragEvent;

        img = GetComponent<Image>();
    }

    void OnClickEvent(PointerEventData eventData)
    {
        if (Item is IClickable)
        {
            Debug.Log("설명(클릭 대응 이벤트)이 있는 아이템입니다.");
        }
        Debug.Log("click");
    }

    void OnDoubleClickEvent(PointerEventData eventData)
    {
        if (Item is IDoubleClickable)
        {
            Debug.Log("장착이 가능(더블클릭 대응 이벤트)한 아이템입니다.");
        }
        Debug.Log("doubleClick");
    }

    void OnDragEvent(PointerEventData eventData)
    {
        //todo? : 확장성 여지.
        //Debug.Log("draging");
    }

}

