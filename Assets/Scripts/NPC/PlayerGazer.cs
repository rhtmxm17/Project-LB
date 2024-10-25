using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGazer : MonoBehaviour
{

    Transform player;

    private void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        Vector3 destPos = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(destPos);
        
    }

}
