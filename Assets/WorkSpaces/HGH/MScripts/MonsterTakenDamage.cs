using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterTakenDamage : MonoBehaviour, IDamageable
{
    [Header("Model")]
    [SerializeField] MonsterModel monsterModel;

    private void Awake()
    {
        monsterModel = GetComponent<MonsterModel>();
    }

    public void Damaged(int damage, DamageType type)
    {
        monsterModel.MonsterHP -= damage;
    }

    private void MonsterDead()
    {
        if (monsterModel.MonsterHP == 0)
        {
            Destroy(gameObject);
        }
    }
}
