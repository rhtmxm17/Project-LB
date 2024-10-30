using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterTakenDamage : MonoBehaviour, IDamageable
{
    [Header("Model")]
    [SerializeField] MonsterModel monsterModel;
    [SerializeField] Animator monsterAni;
    [SerializeField] Rigidbody rigid;
    [SerializeField] NavMeshAgent monsterAgent;
        

    private void Awake()
    {
        monsterModel = GetComponent<MonsterModel>();
        monsterAni = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
        monsterAgent = GetComponent<NavMeshAgent>();
    }


    public void Damaged(int damage, DamageType type)
    {
        monsterAni.SetTrigger("TakenDamaged");
        // 몬스터 HP가 damage만큼 감소
        monsterModel.MonsterCurHP -= damage;
        MonsterDead();
    }

    private void MonsterDead()
    {
        if (monsterModel.MonsterCurHP <= 0)
        {
            monsterAni.SetTrigger("DeadTrigger");
            // 몬스터 사망시
            Destroy(gameObject, 1f);
        }
    }

    public void MosterTakenKnockBack(Vector3 power)
    {
        // 몬스터가 power 방향+힘으로 밀려남
        monsterAgent.Move(power);
        Debug.Log("몬스터가 뒤로 밀려납니다");
    }
}
