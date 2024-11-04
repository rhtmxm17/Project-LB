using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterVFX : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] ParticleSystem specterVFX;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioEffect;
    // [SerializeField] MonsterModel monsterModel;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void ReviveVFX()
    {
        specterVFX.Play();
        audioSource.PlayOneShot(audioEffect);
    }
}
