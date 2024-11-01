using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerArea : MonoBehaviour
{

    [SerializeField]
    WaveManager waveManager;

    [SerializeField]
    public UnityEvent onTriggerEvent;



    bool isTriggered = false;

    private void OnTriggerEnter(Collider other)
    {

        if (!isTriggered && other.gameObject.tag == "Player")
        {
            isTriggered = true;
            waveManager?.StartWaveTrigger();
            onTriggerEvent?.Invoke();
        }

    }

}
