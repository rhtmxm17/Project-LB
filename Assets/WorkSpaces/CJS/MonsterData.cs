using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MonsterData")]
public class MonsterData : ScriptableObject
{
    /// <summary>
    /// 구현된 몬스터 기능
    /// </summary>
    public MonsterModel MonsterPrefab => monsterPrefab;
    [SerializeField, Tooltip("해당 데이터를 적용할 프리팹")] MonsterModel monsterPrefab;

    public float AttackRange => attackRange;
    [SerializeField] float attackRange = 1f;

    public int AttackDamage => attackDamage;
    [SerializeField] int attackDamage = 10;

    public int MaxHp => maxHp;
    [SerializeField] int maxHp = 300;

    public float AttackPeriod => 1f / attckPerSecond;
    [SerializeField] float attckPerSecond = 1f;

    public float MoveSpeed => moveSpeed;
    [SerializeField] float moveSpeed = 1f;
}
