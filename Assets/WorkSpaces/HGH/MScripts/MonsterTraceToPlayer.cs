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
    [SerializeField] Animator monsterAni;

    [Header("Property")]
    [SerializeField] float maxDistance;
    [SerializeField] float distanceStopping;
    // [SerializeField] float chaseSpeed;
    private bool isChecked;

    [field: SerializeField] public bool IsSpawnerBased { get; set; } = false;
    private MonsterModel monsterModel;

    private void Awake()
    {
        monsterAgent = GetComponent<NavMeshAgent>();
        monsterModel = GetComponent<MonsterModel>();
        player = GameObject.FindGameObjectWithTag("Player");
        isChecked = false;
        distanceStopping = 1f;

        if (monsterAni == null)
        {
            monsterAni = GetComponent<Animator>();
        }
    }

    private void Start()
    {
        PlayerModel playerModel = GameManager.Instance.GetPlayerModel();
        monsterModel.OnInit += Init;

        // 몬스터가 플레이어에게 가까이 다가갈때 어느 정도에서 멈출것인가
        monsterAgent.stoppingDistance = distanceStopping;
        maxDistance = 100;
    }

    private void OnDestroy()
    {
        monsterModel.OnInit -= Init;
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * maxDistance, Color.red);
        ChaseOn();

        if (isChecked == true)
        {
            monsterAni.SetBool("isChaseOn", true);
            monsterAgent.destination = player.transform.position;
        }
    }

    private void Init()
    {
        monsterAgent.speed = monsterModel.DataTable.MoveSpeed;
    }

    public void ChaseOn()
    {
        if (IsSpawnerBased || // 조건1: 스포너에서 생성되었거나
            ((this.transform.position - player.transform.position).sqrMagnitude < maxDistance * maxDistance)) // 조건2: 플레이어와 거리가 maxDistance 미만이거나
        {
            monsterAni.SetBool("isChaseOn", true);
            //// 플레이어를 시야에서 놓치지 않게 플레이어를 계속 쳐다보게
            //// NavMeshAgent가 자동으로 회전시키는 것을 사용하기 위해 주석 처리
            //transform.LookAt(player.transform.position);
            // 그러면 몬스터는 플레이어 위치로 NaviMesh로 이동한다
            monsterAgent.destination = player.transform.position;

            isChecked = true;
        }

        /*
        // RaycastAll로 레이를 발사해서 벽에 가로막혀도 플레이어를 찾을 수 있게 함
        RaycastHit[] hit;
        hit = Physics.RaycastAll(transform.position, transform.forward, maxDistance);
        for (int i = 0; i < hit.Length; i++)
        {
            if (hit[i].collider.CompareTag("Player"))
            {
                monsterAni.SetBool("isChaseOn", true);
                // 플레이어를 시야에서 놓치지 않게 플레이어를 계속 쳐다보게
                transform.LookAt(player.transform.position);
                // 그러면 몬스터는 플레이어 위치로 NaviMesh로 이동한다
                monsterAgent.destination = player.transform.position;

                isChecked = true;
            }
        }
        */
    }
}
