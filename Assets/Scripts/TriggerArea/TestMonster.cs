using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class TestMonster : MonoBehaviour
{

    NavMeshAgent agent;
    Transform player;

    Coroutine trackCoroutine = null;

    // Start is called before the first frame update
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    public void ChangeToActive() {

        if (trackCoroutine == null)
        {
            trackCoroutine = StartCoroutine(SetTrackingCo());
        }

    }

    private void OnDisable()
    {
        if (trackCoroutine != null) {
            StopCoroutine(trackCoroutine);
            trackCoroutine = null;
        }
    }

    IEnumerator SetTrackingCo() {

        while (true)
        {
            agent.SetDestination(player.position);
            yield return new WaitForSeconds(0.5f);
        }

    }

    void EndTracking() { 
    
        StopCoroutine(trackCoroutine);
        trackCoroutine = null;
        agent.SetDestination(transform.position);

    }



}
