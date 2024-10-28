using UnityEngine;

/// <summary>
/// 공격 타입에 대한 enum입니다.
/// 특별한 기능이 없는 일반 근접 공격이라면 DEFAULT_MELEE_ATTACK(0) 을 사용합니다.
/// </summary>
public enum DamageType
{
    DEFAULT_MELEE_ATTACK = 0,
    IGNORE_MELEE_IMMUNITY = 1,
}

public interface IDamageable
{
    public Transform transform { get; }

    /// <summary>
    /// 데미지 발생시 공격자측에서 피격자의 Damaged를 호출한다
    /// </summary>
    /// <param name="damage">데미지 수치</param>
    /// <param name="type"></param>
    public void Damaged(int damage, DamageType type);
}
