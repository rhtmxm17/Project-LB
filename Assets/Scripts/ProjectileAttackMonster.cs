using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터가 플레이어를 추적한다는 것을 전제로 원거리 공격 기능 구성
/// 투사체가 가로막히는지 검사하고 공격
/// </summary>
public class ProjectileAttackMonster : MonoBehaviour
{
    [SerializeField] Animator monsterAni;
    [SerializeField] Transform shotPose;
    [SerializeField] MonsterProjectile attackProjectilePrefab;
    [SerializeField, Tooltip("투사체가 플레이어에 닿는지 확인하는 주기")] float checkFiringLinePeriod = 0.1f; // 사선(射線) 검사 주기
    [SerializeField, Tooltip("투사체 검사에 사용될 반경")] float projectileRadius;
    [SerializeField, Tooltip("공격 선딜레이")] float preDelay;

    private PlayerModel playerModel;
    private MonsterModel monsterModel;

    private Coroutine stateRoutine;
    private YieldInstruction sphereCastPeriod;
    private YieldInstruction preDelayWait;
    private YieldInstruction postDelayWait;
    private LayerMask castLayer;

    private AudioSource audioSource;

    private void Awake()
    {
        monsterModel = GetComponent<MonsterModel>();
        if (monsterAni == null)
        {
            if (! TryGetComponent(out monsterAni))
            {
                Debug.LogWarning("몬스터에 Animator가 등록되지 않음");
            }
        }
        audioSource = GetComponent<AudioSource>();

        sphereCastPeriod = new WaitForSeconds(checkFiringLinePeriod);
        preDelayWait = new WaitForSeconds(preDelay);
        castLayer = LayerMask.GetMask("Default", "Player");
    }



    private void Start()
    {
        playerModel = GameManager.Instance.GetPlayerModel();
        monsterModel.OnInit += Init;
    }

    private void OnDestroy()
    {
        monsterModel.OnInit -= Init;

    }

    private void Init()
    {
        postDelayWait = new WaitForSeconds(monsterModel.DataTable.AttackPeriod - preDelay);
    }

    private void OnEnable()
    {
        stateRoutine = StartCoroutine(CheckFiringLine());
    }

    private void OnDisable()
    {
        StopCoroutine(stateRoutine);
    }

    private IEnumerator CheckFiringLine()
    {
        while (true)
        {
            yield return sphereCastPeriod;

            Vector3 dircetion = (playerModel.transform.position - shotPose.position).normalized;

            if (Physics.SphereCast(shotPose.position, projectileRadius, dircetion, out RaycastHit hitInfo, monsterModel.DataTable.AttackRange, castLayer))
            {
                // TODO: 캐스팅 결과 확인
                if (hitInfo.collider.CompareTag("PlayerCollider"))
                {
                    StopCoroutine(stateRoutine);
                    stateRoutine = StartCoroutine(ShotProjectile());
                }
            }
        }
    }

    private IEnumerator ShotProjectile()
    {
        monsterAni.SetTrigger("StartThrow");

        yield return preDelayWait;

        if (monsterModel.DataTable.AttackClip != null)
        {
            audioSource.PlayOneShot(monsterModel.DataTable.AttackClip, monsterModel.DataTable.AttackVolumeScale);
        }

        MonsterProjectile prefabInstance = Instantiate(attackProjectilePrefab, shotPose.position, shotPose.rotation);
        prefabInstance.AttackPoint = monsterModel.DataTable.AttackDamage;
        prefabInstance.SetDestination(playerModel.transform.position + Vector3.up);

        yield return postDelayWait;

        // 공격 완료 후 다시 추적 루틴 돌입
        StopCoroutine(stateRoutine);
        stateRoutine = StartCoroutine(CheckFiringLine());
    }
}
