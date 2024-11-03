using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 몬스터가 플레이어를 추적한다는 것을 전제로 원거리 공격 기능 구성
/// 투사체가 가로막히는지 검사하고 공격
/// </summary>
public class ProjectileMonsterAttack : MonoBehaviour
{
    [SerializeField] Animator monsterAni;
    [SerializeField] Transform shotPose;
    [SerializeField] Rigidbody attackProjectilePrefab;
    [SerializeField, Tooltip("투사체가 플레이어에 닿는지 확인하는 주기")] float checkFiringLinePeriod = 0.1f; // 사선(射線) 검사 주기
    [SerializeField, Tooltip("투사체 검사에 사용될 반경")] float projectileRadius;

    private PlayerModel playerModel;
    private MonsterModel monsterModel;

    private Coroutine stateRoutine;
    private YieldInstruction sphereCastPeriod;
    private LayerMask castLayer;

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

        sphereCastPeriod = new WaitForSeconds(checkFiringLinePeriod);
        castLayer = LayerMask.GetMask("Default", "Player");
    }

    private void Start()
    {
        playerModel = GameManager.Instance.GetPlayerModel();
    }

    private IEnumerator CheckFiringLine()
    {
        while (true)
        {
            yield return sphereCastPeriod;



            if (Physics.SphereCast(shotPose.position, projectileRadius, playerModel.transform.position - shotPose.position, out RaycastHit hitInfo, monsterModel.DataTable.AttackRange, castLayer))
            {
                // TODO: 캐스팅 결과 확인
            }
        }
    }
}
