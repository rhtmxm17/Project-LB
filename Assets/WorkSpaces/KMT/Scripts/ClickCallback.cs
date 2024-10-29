using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ClickCallback : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler
{

    [SerializeField]
    protected bool useDoubleClick;
    [SerializeField]
    protected float doubleClickThreshold;

    Coroutine doubleClickCoroutine = null;
    WaitForSeconds doubleClickWait;

    protected event Action<PointerEventData> ClickEvent;
    protected event Action<PointerEventData> DoubleClickEvent;
    protected event Action<PointerEventData> DragEvent;

    protected virtual void Awake()
    {
        doubleClickWait = new WaitForSeconds(doubleClickThreshold);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (doubleClickCoroutine == null)
        {
            ClickEvent?.Invoke(eventData);

            if(useDoubleClick) 
                doubleClickCoroutine = StartCoroutine(DoubleClickCO());
        }
        else
        {
            DoubleClickEvent?.Invoke(eventData);

            StopCoroutine(doubleClickCoroutine);
            doubleClickCoroutine = null;
        }
    }

    IEnumerator DoubleClickCO() {

        yield return doubleClickWait;
        doubleClickCoroutine = null;

    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (doubleClickCoroutine != null)
        {
            StopCoroutine(doubleClickCoroutine);
            doubleClickCoroutine = null;
        }
    }


    public void OnDrag(PointerEventData eventData)
    {
        DragEvent?.Invoke(eventData);
    }
}
