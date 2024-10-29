using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ClickCallback : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler
{

    [SerializeField]
    bool useDoubleClick;
    [SerializeField]
    float doubleClickThreshold;

    Coroutine doubleClickCoroutine = null;
    WaitForSeconds doubleClickWait;

    public UnityEvent<PointerEventData> ClickEvent;
    public UnityEvent<PointerEventData> DoubleClickEvent;
    public UnityEvent<PointerEventData> DragEvent;

    private void Awake()
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
