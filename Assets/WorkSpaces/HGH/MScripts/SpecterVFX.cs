using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterVFX : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] ParticleSystem specterVFX;
    [SerializeField] GameObject prefab;

    private void Awake()
    {
        
    }

    private void ReviveVFX()
    {
        specterVFX.Play();

    }
}
