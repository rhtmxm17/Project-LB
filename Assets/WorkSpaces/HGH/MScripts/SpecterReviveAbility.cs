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
        if (monsterModel.MonsterCurHP <= 0)
        {
            if (SpecterAbilityStart == null)
            {
                SpecterAbilityStart = StartCoroutine(SpecterAbility());
            }
            
        }
    }

    private void OnDisable()
    {
        StopCoroutine(SpecterAbilityStart);
        SpecterAbilityStart = null;
    }

    public void ReviveAbility()
    {
        Instantiate(prefab, target.position, target.rotation);
    }

    Coroutine SpecterAbilityStart;

    IEnumerator SpecterAbility()
    {
        specterVFX.ReviveVFX();
        yield return new WaitForSeconds(0.3f);
        ReviveAbility();
        yield return new WaitForSeconds(0.3f);
    }
}
