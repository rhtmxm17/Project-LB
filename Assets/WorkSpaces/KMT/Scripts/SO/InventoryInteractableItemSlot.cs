using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image), typeof(ClickCallback))]
public class InventoryInteractableItemSlot : InventoryItemSlot
{

    [SerializeField]
    HotKeySystem hotKeySystem;

    ClickCallback clickCallback;

    protected override void Awake()
    {
        base.Awake();

        clickCallback = GetComponent<ClickCallback>();
        clickCallback.ClickEvent.AddListener(OnClickEvent);
        clickCallback.DoubleClickEvent.AddListener(OnDoubleClickEvent);
        clickCallback.DragEvent.AddListener(OnDragEvent);

    }

    void OnClickEvent(PointerEventData eventData)
    {
        if (Item is IClickable)
        {
            Debug.Log("설명(클릭 대응 이벤트)이 있는 아이템입니다.");
            ((IClickable)Item).OnClickEvent(eventData);
        }
        Debug.Log("click");
    }

    void OnDoubleClickEvent(PointerEventData eventData)
    {
        if (Item is IDoubleClickable)
        {
            Debug.Log("장착이 가능(더블클릭 대응 이벤트)한 아이템입니다.");
            ((IDoubleClickable)Item).OnDoubleClickEvent(eventData);

            hotKeySystem.EquipItem(this);
        }
        Debug.Log("doubleClick");
    }

    void OnDragEvent(PointerEventData eventData)
    {
        //todo? : 확장성 여지.
        //Debug.Log("draging");
    }

}

