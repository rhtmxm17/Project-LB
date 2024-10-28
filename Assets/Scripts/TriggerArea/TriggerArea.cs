using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerArea : MonoBehaviour
{

    [SerializeField]
    UnityEvent onTriggerEvent;

    bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {

        if (!isTriggered && other.gameObject.tag == "Player")
        {
            isTriggered = true;
            onTriggerEvent?.Invoke();
        }

    }

}
