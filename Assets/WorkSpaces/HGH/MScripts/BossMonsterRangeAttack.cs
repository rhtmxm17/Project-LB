using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BossMonsterRangeAttack : AttackMonster
{
    [SerializeField]
    float startRangeCoolDownSec;//5초
    [SerializeField]
    float rangeAttackDelay;

    [SerializeField]
    float distance;

    [SerializeField, Tooltip("공격 선딜레이")] float preDelay;
    [SerializeField] MonsterProjectile attackProjectilePrefab;
    [SerializeField] Transform shotPose;

    public bool IsCring { get; set; } = false;

    NavMeshAgent nav;

    WaitForSeconds coolTime;

    Coroutine rangeAttackCoroutine;

    protected override void Start()
    {
        base.Start();
        coolTime = new WaitForSeconds(rangeAttackDelay);
        rangeAttackCoroutine = StartCoroutine(RangeAttackCO());

        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        Debug.DrawRay(transform.position, (playerModel.transform.position - transform.position).normalized * distance, Color.blue);
    }

    protected override IEnumerator MonsterAutoAttack()
    {
        WaitForSeconds period = new WaitForSeconds(monsterModel.DataTable.AttackPeriod);

        while (true)
        {
            /*            // 몬스터 공격 딜레이 1초 -> 테이블에서 가져오기
                        yield return period;*/

            if (IsCring)
            {
                yield return null;
                continue;
            }

            // 몬스터 공격시작
            monsterAni.SetTrigger("StartAttack");

            StartCoroutine(TestAttack());

            yield return period;
        }
    }

    IEnumerator TestAttack()
    {
        yield return new WaitForSeconds(1.03f);

        nav.speed = 0;

        if (Vector3.Distance(playerModel.transform.position,transform.position) < distance)
        {

            Debug.Log("범위에 있어요 공격할게요!");
            // 개발도중 Debug.Log 확인
            Debug.Log($"{monsterModel.MonsterAP}만큼의 몬스터 공격력으로 플레이어를 공격했다");

            // playerModel의 Hp를 monsterModel의 공격력만큼 감소
            player.Damaged(monsterModel.MonsterAP, DamageType.DEFAULT_MELEE_ATTACK);

            // 개발도중 Debug.Log 확인
            Debug.Log($"플레이어는 {playerModel.Hp}만큼 HP가 남았다");
            // 몬스터 공격 딜레이 1초 -> 테이블에서 가져오기
        }



        yield return new WaitForSeconds(2f);
        nav.speed = 7;
    }


    IEnumerator RangeAttackCO()
    {

        yield return new WaitForSeconds(startRangeCoolDownSec);

        while (true)
        {
            if (startMonsterAttackRoutine == null)
            {
                if (IsCring)
                {
                    yield return null;
                    continue;
                }
                nav.speed = 0;
                monsterAni.Play("Casting Spell");
                yield return new WaitForSeconds(preDelay);

                //if (monsterModel.DataTable.AttackClip != null)
                //{
                //    audioSource.PlayOneShot(monsterModel.DataTable.AttackClip, monsterModel.DataTable.AttackVolumeScale);
                //}

                MonsterProjectile prefabInstance = Instantiate(attackProjectilePrefab, shotPose.position, shotPose.rotation);
                prefabInstance.AttackPoint = monsterModel.DataTable.AttackDamage;
                prefabInstance.SetDestination(playerModel.transform.position + Vector3.up);

                yield return new WaitForSeconds(2f - preDelay);
                nav.speed = 7;
                yield return coolTime;
            }
            else
            {
                yield return null;
            }
        }

    }

}
