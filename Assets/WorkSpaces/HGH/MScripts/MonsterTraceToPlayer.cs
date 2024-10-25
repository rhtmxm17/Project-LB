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
        // 몬스터가 쫓아가는 속도 조정
        monsterAgent.speed = chaseSpeed;
        // 몬스터가 플레이어에게 가까이 다가갈때 어느 정도에서 멈출것인가
        monsterAgent.stoppingDistance = distanceStopping;
        // 레이캐스트 발사
        bool isResult =  Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, maxDistance);
        if (isResult == true)
        {
            // 태그가 플레이어인 오브젝트에 레이캐스트가 부딪히면 
            if (hit.collider.gameObject.CompareTag("Player"))
            {
                // 그러면 몬스터는 플레이어 위치로 NaviMesh로 이동한다
                monsterAgent.destination = player.transform.position;
            }
        }
    }
}
