using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    public void PlayBGM(AudioClip clip, float volumeScale = 1f)
    {
        bgmSource.clip = clip;
        bgmSource.volume = volumeScale;
        bgmSource.Play();
    }

    public void StopBGM()
    {
        bgmSource.Stop();
    }


    // 효과음 출력과 동시에 비활성화 또는 소멸하는 오브젝트의 효과음 출력 대행
    public void PlaySFX(AudioClip clip, float volumeScale = 1f) => sfxSource.PlayOneShot(clip, volumeScale);
}
