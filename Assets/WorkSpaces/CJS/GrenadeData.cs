using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지별로 구분될 수류탄 정보
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/GrenadeData")]
public class GrenadeData : ScriptableObject
{
    /// <summary>
    /// 공격 가능한 대상 Layer
    /// </summary>
    public LayerMask TargetLayer => targetLayer;
    [SerializeField] LayerMask targetLayer;
    
    /// <summary>
    /// 폭발 범위
    /// </summary>
    public float ExplosionRange => explosionRange;
    [SerializeField] float explosionRange = 3f;

    /// <summary>
    /// 피해량
    /// </summary>
    public int Damage => damage;
    [SerializeField] int damage = 300;

    public float MaxChargeTime => maxChargeTime;
    [SerializeField] float maxChargeTime;

    public float MinThrowForce => minThrowForce;
    [SerializeField] float minThrowForce;

    public float MaxThrowForce => maxThrowForce;
    [SerializeField] float maxThrowForce;
}
