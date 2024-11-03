using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventInteractable : MonoBehaviour, IInteractable
{
    /// <summary>
    /// 플레이어가 해당 물체에 상효작용 했을 때 실행되는 이벤트
    /// </summary>
    public UnityEvent OnPlayerInteracted;

    public void OnInteracted()
    {
        OnPlayerInteracted?.Invoke();
    }
}
