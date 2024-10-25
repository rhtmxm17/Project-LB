using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosnterLookAt : MonoBehaviour
{
    [SerializeField] Transform playerPosition;

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.LookAt(playerPosition);
        }
    }
}
