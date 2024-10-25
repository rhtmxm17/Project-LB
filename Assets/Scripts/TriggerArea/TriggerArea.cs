using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerArea : MonoBehaviour
{

    [SerializeField]
    UnityEvent onTriggerEvent;


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player")
        {
            onTriggerEvent?.Invoke();
        }

    }

}
