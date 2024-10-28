using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    private bool isChecked;

    bool isTracked;

    private void Awake()
    {
        monsterAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player");
        isChecked = false;
    }

    private void Start()
    {
        PlayerModel playerModel = GameManager.Instance.GetPlayerModel();
        // 몬스터가 쫓아가는 속도 조정
        monsterAgent.speed = chaseSpeed;

        // 몬스터가 플레이어에게 가까이 다가갈때 어느 정도에서 멈출것인가
        monsterAgent.stoppingDistance = distanceStopping;

        maxDistance = 100;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.red);
        ChaseOn();

        if (isChecked == true)
        {
            monsterAgent.destination = player.transform.position;
        }
    }

    public void ChaseOn()
    {
        // RaycastAll로 레이를 발사해서 벽에 가로막혀도 플레이어를 찾을 수 있게 함
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position, transform.forward, maxDistance);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("Player"))
            {
                // 플레이어를 시야에서 놓치지 않게 플레이어를 계속 쳐다보게
                transform.LookAt(player.transform.position);
                // 그러면 몬스터는 플레이어 위치로 NaviMesh로 이동한다
                monsterAgent.destination = player.transform.position;

                isChecked = true;
            }
        }
    }
}
