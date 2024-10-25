using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterTraceToPlayer : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] NavMeshAgent monsterAgent;
    [SerializeField] GameObject player;

    [Header("Property")]
    [SerializeField] float maxDistance;
    [SerializeField] float distanceStopping;
    [SerializeField] float chaseSpeed;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.red);
        ChaseOn();
    }

    public void ChaseOn()
    {
        monsterAgent.speed = chaseSpeed;
        monsterAgent.stoppingDistance = distanceStopping;
        bool isResult =  Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance);
        if (isResult == true)
        {
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                monsterAgent.destination = player.transform.position;
            }
        }
    }
}
