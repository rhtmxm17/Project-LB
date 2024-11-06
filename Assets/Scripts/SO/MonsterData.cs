using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MonsterData")]
public class MonsterData : ScriptableObject
{
    public string MonsterName => monsterName;
    [SerializeField] string monsterName;

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

    public AudioClip AttackClip => attackClip; // 공격 소리
    [SerializeField] AudioClip attackClip;

    public float AttackVolumeScale => attackVolumeScale;
    [SerializeField] float attackVolumeScale = 1f; // 공격 소리 음량

    // TODO:
    // 생성시 소리... MonsterModel OnEnable()
    // 사망시 소리... MonsterTakenDamage의 실제로 죽는게 확정된 위치 찾아서
    // AttackMonster에 사운드 넣은 것 참고

    public AudioClip SpawnClip => spawnClip; // 스폰 소리
    [SerializeField] AudioClip spawnClip;

    public float SpawnVolumeScale => spawnVolumeScale;
    [SerializeField] float spawnVolumeScale = 1f; // 스폰 소리 음량

    public AudioClip DeadClip => deadClip; // 죽는 소리
    [SerializeField] AudioClip deadClip;

    public float DeadVolumeScale => deadVolumeScale;
    [SerializeField] float deadVolumeScale = 1f; // 죽는 소리 음량
}
