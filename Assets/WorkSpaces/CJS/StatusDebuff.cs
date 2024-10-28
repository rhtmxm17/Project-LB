using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 디버프 클래스<br/>
/// 일반적으로 디버프 지속시간을 가진다.
/// 디버프 지속시간이 0 이하일 경우 디버프 부여시 별도의 해제 조건을 UnityAction으로 추가한다.
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObject/StatusDebuff")]
public class StatusDebuff : ScriptableObject
{
    //public enum Status
    //{
    //    MOVE_SPEED,
    //}

    //public Status TargetStatus => targetStatus;
    //[SerializeField, Tooltip("디버프 대상 스텟")] Status targetStatus;

    public float EffectMultiplier => effectMultiplier;
    [SerializeField, Tooltip("디버프 적용시 스텟에 곱해질 값")] float effectMultiplier;

    public float Duration => duration;
    [SerializeField, Tooltip("디버프 지속 시간")] float duration;

}
