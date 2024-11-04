using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterReviveAbility : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] GameObject prefab;
    [SerializeField] Transform target;
    [SerializeField] SpecterVFX specterVFX;
    [SerializeField] MonsterModel monsterModel;

    private void Update()
    {
        // 스펙터 HP가 0 이하가 되면 어빌리티CoRoutine이 시작
        if (monsterModel.MonsterCurHP <= 0)
        {
            if (SpecterAbilityStart == null)
            {
                SpecterAbilityStart = StartCoroutine(SpecterAbility());
            }
            
        }
    }

    // 스펙터가 사라지면 코루틴 정지
    private void OnDisable()
    {
        StopCoroutine(SpecterAbilityStart);
        SpecterAbilityStart = null;
    }

    // 스펙터가 부활되는 능력
    public void ReviveAbility()
    {
        Instantiate(prefab, target.position, target.rotation);
    }

    // 스펙터 코루틴 
    Coroutine SpecterAbilityStart;

    IEnumerator SpecterAbility()
    {
        // 스펙터의 어빌리티가 시작될때 이펙트 재생
        specterVFX.ReviveVFX();
        yield return new WaitForSeconds(0.3f);
        // 스펙터 어빌리티 함수 시작
        ReviveAbility();
        yield return new WaitForSeconds(0.3f);
    }
}
