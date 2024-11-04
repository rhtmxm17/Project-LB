using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// 충돌 시에만 획득 가능한 수집아이템 입니다.
/// 하위 클래스는 힐팩, 설계도, 일지가 있습니다.
/// </summary>
public class Collection : Item, IInteractable
{
    /// <summary>
    /// 플레이어가 상호작용시 발생하는 이벤트입니다.
    /// </summary>
    public UnityEvent OnPickup;

    [SerializeField] AudioClip audioClip;

    public void OnInteracted()
    {
        // IInteractable 구현입니다
        PickupSound();

        Pickup();

        OnPickup?.Invoke();
    }

    /// <summary>
    /// 플레이어가 아이템 습득시, 공통적으로 수행될 함수입니다.
    /// </summary>
    protected void PickupSound()
    {
        if (audioClip != null)
        {
            GameManager.Instance.GetSoundManager().PlaySFX(audioClip);
        }
    }

    protected virtual void Pickup()
    {
        gameObject.SetActive(false);
    }

}
