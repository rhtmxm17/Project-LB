using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class MeleeWeaponBase : MonoBehaviour, IUseable
{
    public class ItemData : ScriptableObject
    {
        public int ID;
        public string Name;
        public int Damage;
        public int Range;
        
    }

    public List<ItemData> items;

    void Start()
    {
        foreach (var item in items)
        {
            Debug.Log($"Item Name : {item.Name}, Damage: {item.Damage}, Range: {item.Range}");                
        }
    }
    
    public enum State  // 상태
    {
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔 Empty(비어있다)
        

    }

    public State state { get; private set; } // 현재 근접무기의 상태

    [SerializeField] Transform muzzleTransform; //muzzle(씌우다) Transform(변형시키다)
    [SerializeField] Transform MeleeweaponCamera;
    [field: SerializeField] public GunData DataTable { get; set; }
    

    private YieldInstruction firePeriod; // Yield(산출,생산하다) Instruction(지시하다) fire(발사) Period(이상,끝)을

    private Coroutine fireRoutine;
    private Coroutine reloadRoutine;

    // 근접무기가 마지막으로 타격한 시간을 기록한다(연타 가속 방지)
    private float lastFireTime;

    private void Awake() // magazine <- 탄창이라는 뜻인데 이거 뺴야하나..? 근데 데이터 테이블 생각하면 으아아아아아 -> 탄창은 필요하지않는것으로 간주하고 삭제처리 함
    {
        firePeriod = new WaitForSeconds(DataTable.timeBetFire);
        lastFireTime = Time.time;
        
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
        // 트레일 등 이펙트를 그리기 위해 피격대상이 맞은 곳을 저장
        Vector3 hitPosition = Vector3.zero;

        Vector3 shotPosition;
        Vector3 range;
        if (MeleeweaponCamera == null)
        {
            shotPosition = muzzleTransform.position;
            range = muzzleTransform.forward;
        }
        else
        {
            // 무기 전용 카메라 사용시 실제 총구 대신 플레이어 눈에 보이는 총기 위치에서 발사
            shotPosition = Camera.main.transform.TransformPoint(MeleeweaponCamera.InverseTransformPoint(muzzleTransform.position));
            range = Camera.main.transform.TransformDirection(MeleeweaponCamera.InverseTransformDirection(muzzleTransform.forward));
        }
        Debug.DrawRay(shotPosition, DataTable.range * range, Color.yellow, 0.1f);

        // 레이캐스트(시작 지점, 방향, 충돌 정보 컨테이너, 사정거리)
        if (Physics.Raycast(shotPosition, range, out RaycastHit hit, DataTable.range, DataTable.layerMask))
        

        // 적중 여부와 무관히 발생하는 처리

        // 발사 이펙트 재생 시작
        StartCoroutine(ShotEffect(hitPosition));


        Collider[] hitColliders = Physics.OverlapSphere(shotPosition, DataTable.range); 
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
        Debug.Log("발사 이펙트 출력 필요");
        yield break;
    }
    
}
