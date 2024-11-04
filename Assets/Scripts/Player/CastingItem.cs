using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 클릭시 시전 시작, 지정된 시간 후 비활성화 상태가 아니라면 OnCasted 호출
/// </summary>
[RequireComponent(typeof(Animator))]
public class CastingItem : MonoBehaviour, IUseable
{
    [SerializeField, Tooltip("사용 완료에 필요한 시간")] float timeRequirement = 0.5f;

    [SerializeField, Tooltip("사용 가능 횟수 제한 없음")] bool isUnlimited = false;

    [field: SerializeField, Tooltip("사용 가능 횟수")] public int Usage { get; set; } = 5;

    public event UnityAction OnCasted;

    private Coroutine drinkRoutine;
    private YieldInstruction drinkWait;

    private Animator animator;
    private int hashShow;

    private void Awake()
    {
        drinkWait = new WaitForSeconds(timeRequirement);
        animator = GetComponent<Animator>();
        hashShow = Animator.StringToHash("Show");
    }

    public void UseBegin()
    {
        if (drinkRoutine == null)
        {
            Debug.Log("이미 캐스팅중입니다");
            return;
        }

        if ((isUnlimited || Usage > 0))
        {

            drinkRoutine = StartCoroutine(Casting());
        }
    }

    public void UseEnd()
    {
    }

    private IEnumerator Casting()
    {
        yield return drinkWait;
        Usage--;
        OnCasted?.Invoke();
        drinkRoutine = null;
    }

    private void OnDisable()
    {
        if (drinkRoutine != null)
            drinkRoutine = null;
    }

    public void ShowAnimation(bool show)
    {
        animator.SetBool(hashShow, show);
    }
}
