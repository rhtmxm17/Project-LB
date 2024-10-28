using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    private float moveSpeedMultifier = 1f; // 디버프 수치 계수, 최소값(가장 강한 디버프)만 적용
    [SerializeField] int maxHp = 100;
    [SerializeField] int hp = 100;

    public float MoveSpeed { get => moveSpeed * moveSpeedMultifier; set => moveSpeed = value; }
    public int MaxHp { get => maxHp; set => maxHp = value; }
    public int Hp { get => hp; set { hp = value; OnHpChanged(); } }

    public event UnityAction OnHpChange;

    private void OnHpChanged()
    {
        if (hp > MaxHp)
            hp = MaxHp;
        
        OnHpChange?.Invoke();
    }

#if UNITY_EDITOR
    private void Awake()
    {
        if (!gameObject.CompareTag("Player"))
            Debug.LogWarning("플레이어의 태그 설정이 누락된 것으로 보임");
    }
#endif //UNITY_EDITOR

    #region Debuff Managemanet

    private struct DebuffInfo
    {
        public StatusDebuff debuff;
        public Coroutine disableRoutine;
    }

    private List<DebuffInfo> debuffs = new List<DebuffInfo>(4);

    /// <summary>
    /// 지정된 디버프를 적용한다.
    /// </summary>
    /// <param name="debuff">디버프 객체</param>
    public void AddDebuff(StatusDebuff debuff)
    {
        int result = debuffs.FindIndex(item => item.debuff == debuff);

        // 시간제한이 있는 디버프일경우 제거 예약
        Coroutine disable = null;
        if (debuff.Duration > 0f)
        {
            disable = StartCoroutine(RemoveDebuffRoutine(debuff));
        }

        DebuffInfo newInfo = new()
        {
            debuff = debuff,
            disableRoutine = disable,
        };

        if (result != -1)
        {
            // 이미 존재하는 디버프라면 지속시간 갱신
            StopCoroutine(debuffs[result].disableRoutine);
            debuffs[result] = newInfo;
        }
        else
        {
            debuffs.Add(newInfo);
        }

        // 기존 디버프보다 수치가 낮다면(강력하다면) 디버프 갱신
        if (moveSpeedMultifier > debuff.EffectMultiplier)
            moveSpeedMultifier = debuff.EffectMultiplier;

        Debug.Log($"디버프 적용 이동속도: {MoveSpeed}");
    }

    private IEnumerator RemoveDebuffRoutine(StatusDebuff debuff)
    {
        yield return new WaitForSeconds(debuff.Duration);

        RemoveDebuff(debuff);
    }

    /// <summary>
    /// 특정 디버프를 제거한다
    /// </summary>
    /// <param name="debuff">제거할 디버프</param>
    public void RemoveDebuff(StatusDebuff debuff)
    {
        int result = debuffs.FindIndex(item => item.debuff == debuff);

        if (result < 0)
        {
            Debug.LogWarning("이미 사라진 디버프의 제거 시도됨");
            return;
        }

        debuffs.RemoveAt(result);

        // 가장 수치가 낮은(강력한) 디버프였을 경우 디버프 상태 갱신
        if (moveSpeedMultifier >= debuff.EffectMultiplier)
        {
            UpdateDebuffState();
        }
    }

    private void UpdateDebuffState()
    {
        moveSpeedMultifier = 1f;
        foreach (var info in debuffs)
        {
            if (moveSpeedMultifier > info.debuff.EffectMultiplier)
                moveSpeedMultifier = info.debuff.EffectMultiplier;
        }
    }

    #endregion Debuff Managemanet

}
