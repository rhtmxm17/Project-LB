using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecterVFX : MonoBehaviour
{
    [Header("GameObject")]
    [SerializeField] ParticleSystem specterVFX;
    [SerializeField] GameObject prefab;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip audioEffect;
    // [SerializeField] MonsterModel monsterModel;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // 스펙터 부활될때 재생되는 이펙트 효과, 사운드 포함
    public void ReviveVFX()
    {
        Instantiate(prefab,transform.position, transform.rotation);
        specterVFX.Play();
        audioSource.PlayOneShot(audioEffect);
    }
}
