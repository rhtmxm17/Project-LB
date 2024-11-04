using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterReviveAbility : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] GameObject prefab;
    [SerializeField] Transform target;
    [SerializeField] SpecterVFX specterVFX;

    private void OnDestroy()
    {
        ReviveAbility();
        specterVFX.ReviveVFX();
    }

    public void ReviveAbility()
    {
        Instantiate(prefab, target.position, target.rotation);
    }
}
