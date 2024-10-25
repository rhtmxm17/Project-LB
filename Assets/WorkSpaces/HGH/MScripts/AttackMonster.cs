using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackMonster : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] PlayerModel playerModel;
    [SerializeField] MonsterModel monsterModel;

    [Header("Property")]
    [SerializeField] float rayDistance;

    private void OnTriggerEnter(Collider other)
    {
        // 코루틴이 null 이라면
        if (startMonsterAttackRoutine == null)
        {
            // 충돌체와 부딪힌 오브젝트의 태그가 Player라면
            if (other.gameObject.CompareTag("Player"))
            {
                // 몬스터의 근접공격 코루틴을 시작시켜라
                startMonsterAttackRoutine = StartCoroutine(MonsterAutoAttack());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 코루틴이 null 이 아니라면
        if (other.gameObject.CompareTag("Player"))
        {
            if (startMonsterAttackRoutine != null)
            {
                // 코루틴 정지시키고
                StopCoroutine(startMonsterAttackRoutine);

                // 코루틴 비워라
                startMonsterAttackRoutine = null;
            }
        }
    }

    public void AttackToPlayer()
    {
        // 개발도중 Debug.Log 확인
        Debug.Log($"{monsterModel.MonsterAP}만큼의 몬스터 공격력으로 플레이어를 공격했다");

        // playerModel의 Hp를 monsterModel의 공격력만큼 감소
        playerModel.Hp -= monsterModel.MonsterAP;

        // 개발도중 Debug.Log 확인
        Debug.Log($"플레이어는 {playerModel.Hp}만큼 HP가 남았다");
    }

    Coroutine startMonsterAttackRoutine;

    IEnumerator MonsterAutoAttack()
    {
        while (true)
        {
            // 몬스터 공격 딜레이 1초
            yield return new WaitForSeconds(1f);
            // 몬스터 공격시작
            AttackToPlayer();
        }
       
    }
}
