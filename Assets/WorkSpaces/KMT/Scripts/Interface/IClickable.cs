using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IClickable
{
    public void OnClickEvent(PointerEventData eventData);
}
