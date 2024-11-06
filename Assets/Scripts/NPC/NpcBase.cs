using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NpcBase : MonoBehaviour
{

    virtual protected void OnTriggerEnterCallback() { }
    virtual protected void OnTriggerExitCallback() { }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnTriggerEnterCallback();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            OnTriggerExitCallback();
        }
    }

}
