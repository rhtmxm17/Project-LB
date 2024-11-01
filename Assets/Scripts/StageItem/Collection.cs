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

    public void OnInteracted()
    {
        // IInteractable 구현입니다
        PickupSound();

        PaPer();

        OnPickup?.Invoke();
    }

    /// <summary>
    /// 플레이어가 아이템과 충돌시, 공통적으로 수행될 함수입니다.
    /// </summary>
    protected void PickupSound()
    {
        // 충돌시, 효과음이 재생된다.
        Debug.Log($"아이템 획득 SF 출력");
              

    }



    /// <summary>    
    /// 힐팩에 해당되는 함수입니다.
    /// </summary>
    protected void Heal()
    {
        // 충돌 즉시 인벤토리로 들어감

        // 자동으로 장착됨, 인게임 UI에 노출

        // 결과와 상관없이 게임이 끝나면 보유한 힐팩 개수가 0이 됨

        // 충돌시, 오브젝트는 바로 사라짐
        //this.gameObject.SetActive(false);

    }



    /// <summary>
    /// 설계도와 일지에 해당되는 함수입니다. 
    /// </summary>
    protected void PaPer()
    {
        // 충돌시 게임이 끝날때까지 가지고 있음

        // 스테이지 클리어 실패하고 사망시 잃어버림 (보유 취소, 맵에 다시 생성됨)

        // 스테이지 클리어 성공시에만 획득되고 맵에서 사라짐 (최초 1회만 습득가능)

        // 충돌시, 오브젝트는 바로 사라짐
        this.gameObject.SetActive(false);

    }

}
