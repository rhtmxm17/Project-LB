using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MosnterLookAt : MonoBehaviour
{
    [SerializeField] GameObject playerPosition;

    private void Awake()
    {
        playerPosition = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.LookAt(playerPosition.transform.position);
        }
    }
}
