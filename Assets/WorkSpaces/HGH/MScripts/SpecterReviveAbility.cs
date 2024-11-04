using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterReviveAbility : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] GameObject prefab;
    [SerializeField] Transform target;

    private void OnDestroy()
    {
        ReviveAbility();
    }

    public void ReviveAbility()
    {
        Instantiate(prefab, target.position, target.rotation);
    }
}
