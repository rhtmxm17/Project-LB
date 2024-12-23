using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterTakenDamage : MonoBehaviour, IDamageable
{
    [Header("Model")]
    [SerializeField] protected MonsterModel monsterModel;
    [SerializeField] protected Animator monsterAni;
    [SerializeField] Rigidbody rigid;
    [SerializeField] NavMeshAgent monsterAgent;
    

    public Action OnDeadEvent = null;
    private AudioSource audioSource;


    protected virtual void Awake()
    {
        monsterModel = GetComponent<MonsterModel>();
        rigid = GetComponent<Rigidbody>();
        monsterAgent = GetComponent<NavMeshAgent>();

        if (monsterAni == null)
        {
            monsterAni = GetComponent<Animator>();
        }
        audioSource = GetComponent<AudioSource>();


    }

    public virtual void Damaged(int damage, DamageType type)
    {
        if (monsterModel.MonsterCurHP <= 0)
            return;

        monsterAni.SetTrigger("TakenDamaged");
        // 몬스터 HP가 damage만큼 감소
        monsterModel.MonsterCurHP -= damage;
        MonsterDead();
    }

    protected virtual void MonsterDead()
    {
        if (monsterModel.MonsterCurHP <= 0)
        {
            // 몬스터 사망 애니메이션 출력
            monsterAgent.isStopped = true;
            // 몬스터의 죽는 모션 트리거
            monsterAni.SetTrigger("DeadTrigger");

            // 몬스터 공격 사운드
            if (monsterModel.DataTable.DeadClip != null)
            {
                audioSource.PlayOneShot(monsterModel.DataTable.DeadClip, monsterModel.DataTable.DeadVolumeScale);
            }

            // 몬스터 사망시
            transform.SetParent(null);
            // 이벤트 호출
            OnDeadEvent?.Invoke();
            // 1초 후 몬스터 삭제
            Destroy(gameObject, 1f);
        }
        
    }

    public void MosterTakenKnockBack(Vector3 power)
    {
        // 몬스터가 power 방향+힘으로 밀려남
        monsterAgent.Move(power);
        // 피격모션 등장, 피격모션 이 부분은 Damaged 함수에 있으니까
        // 중복된다고 생각하여 주석처리 했습니다.
        // monsterAni.SetTrigger("TakenDamaged");
        Debug.Log("몬스터가 뒤로 밀려납니다");
    }
}
