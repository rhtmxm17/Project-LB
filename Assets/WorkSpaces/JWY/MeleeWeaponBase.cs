using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class MeleeWeaponBase : MonoBehaviour, IUseable
{
    public enum State // 상태
    {
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔
        
    }

    public State state { get; private set; } // 현재 근접무기의 상태

    [SerializeField] Transform muzzleTransform; //muzzle(씌우다) Transform(변형시키다) 
    [field: SerializeField] public MeleeWeaponData DataTable { get; set; }

    public int MagazineCapacity => DataTable.magCapacity; // Magazine(탄창) Capacity(용량)
    public int MagazineRemain => magazineRemain;

    private int magazineRemain;
    private YieldInstruction firePeriod; // Yield(산출,생산하다) Instruction(지시하다) fire(발사) Period(이상,끝)을

    private Coroutine fireRoutine;
    private Coroutine reloadRoutine;

    // 근접무기가 마지막으로 타격한 시간을 기록한다(연타 가속 방지)
    private float lastFireTime;

    private void Awake() // magazine <- 탄창이라는 뜻인데 이거 뺴야하나..? 근데 데이터 테이블 생각하면 으아아아아아
    {
        firePeriod = new WaitForSeconds(DataTable.timeBetFire);
        lastFireTime = Time.time;
        magazineRemain = DataTable.magCapacity; // Capacity <- 용량
    }

    public void UseBegin()
    {
        Debug.Log("MeleeWeapon UseBegin");                   // Use쓰다       Begin시작하다
        fireRoutine = StartCoroutine(Fire());   // 코루틴을 쓰기(Use) 시작하다(Begin)
    }

    public void UseEnd()
    {
        Debug.Log("MeleeWeapon UseEnd");
        if (fireRoutine != null)                // null(아무가치없는,무효의)
        {
            StopCoroutine(fireRoutine);
        }
    }

    private IEnumerator Fire() // 발사
    {
        // 연타 가속 방지
        float canFireTime = lastFireTime + DataTable.timeBetFire;
        if (canFireTime > Time.time)
        {
            yield return new WaitForSeconds(canFireTime - Time.time);
        }

        while (state == State.Ready)        //while(하는동안에) State(상태)의Ready(시작)
        {
            lastFireTime = Time.time;
            Shot();
            yield return firePeriod;    // Period(이상,끝)
        }
        
    }

    private void Shot()
    {
        throw new NotImplementedException(); // <- 이거 안만들면 위에 While에서Shot이 오류가남 
    }


    public void Shot(Vector3 position, float range) // center를position으로 radius를range으로 변경함
    {
        Collider[] hitColliders = Physics.OverlapSphere(position, range); 
        int i = 0;
        while (i < hitColliders.Length) // hit Colliders(충돌체들) 의 Length(길이) 
        {
            hitColliders[i].SendMessage("AddDamage"); // 데미지 메세지 송출
            i++;
        }
    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        // TODO: 이펙트 처리
        yield break;
    }
    
}
