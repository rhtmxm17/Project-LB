using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTakenDamage : MonoBehaviour, IDamageable
{
    [Header("Model")]
    [SerializeField] MonsterModel monsterModel;
    [SerializeField] Animator monsterAni;

    private void Awake()
    {
        monsterModel = GetComponent<MonsterModel>();
        monsterAni = GetComponent<Animator>();
    }

    public void Damaged(int damage, DamageType type)
    {
        monsterAni.SetTrigger("TakenDamage");
        // 몬스터 HP가 damage만큼 감소
        monsterModel.MonsterCurHP -= damage;
        MonsterDead();
    }

    private void MonsterDead()
    {
        if (monsterModel.MonsterHP <= 0)
        {
            monsterAni.SetTrigger("DeadTrigger");
            // 몬스터 사망시
            Destroy(gameObject, 1.5f);
        }
    }
}
