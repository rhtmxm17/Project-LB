using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTakenDamage : MonoBehaviour, IDamageable
{
    [Header("Model")]
    [SerializeField] MonsterModel monsterModel;
    [SerializeField] PlayerModel playerModel;

    // [Header("Property")]
    

    private void Awake()
    {
        monsterModel = GetComponent<MonsterModel>();
    }

    public void Damaged(int damage, DamageType type)
    {
        // 몬스터 HP가 damage만큼 감소
        monsterModel.MonsterHP -= damage;
        MonsterDead();
    }

    private void MonsterDead()
    {
        if (monsterModel.MonsterHP <= 0)
        {
            // 몬스터 사망시
            Destroy(gameObject);
        }
    }
}
