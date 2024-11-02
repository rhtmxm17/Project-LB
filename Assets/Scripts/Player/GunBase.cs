using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class GunBase : MonoBehaviour, IUseable
{
    public enum State
    {
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔
        Reloading // 재장전 중
    }

    public State CurrentState { get; protected set; } // 현재 총의 상태

    [SerializeField, Tooltip("공격 판정에 사용될 기점 위치")] Transform muzzleTransform;
    [SerializeField, Tooltip("총구 이펙트")] ParticleSystem muzzleParticle;
    [field: SerializeField] public Transform WeaponCamera { get; set; }

    public Vector3 ShotPosition
    {
        get
        {
            if (WeaponCamera == null)
                return muzzleTransform.position;
            else
                return Camera.main.transform.TransformPoint(WeaponCamera.InverseTransformPoint(muzzleTransform.position));
        }
    }

    public Vector3 ShotDircetion
    {
        get
        {
            if (WeaponCamera == null)
                return muzzleTransform.forward;
            else
                return Camera.main.transform.forward;
        }
    }

    public int AttackPoint => GunLevel * DataTable.AdditiveAttackPoint + DataTable.AttackPoint;

    public InventoryEquableItemSO DataTable
    {
        get => dataTable;
        set
        {
            dataTable = value;
            InitTableData();
        }
    }
    [SerializeField] InventoryEquableItemSO dataTable;

    public event UnityAction OnShot;
    [field: SerializeField] public int GunLevel { get; set; } = 0;

    /// <summary>
    /// 탄창의 크기
    /// </summary>
    public int MagazineCapacity => DataTable.MagCapacity;

    /// <summary>
    /// 탄창에 남아있는 탄약
    /// </summary>
    public int MagazineRemain { get => magazineRemain; protected set => magazineRemain = value; }

    /// <summary>
    /// 재장전시 소비되는 남은 소지 탄약
    /// </summary>
    public int BulletStock { get => bulletStock; } 


    private int magazineRemain; // 장전된 탄약, 발사시 소비
    private int bulletStock; // 남은 소지 탄약, 재장전시 소비
    private YieldInstruction firePeriod;

    private Coroutine fireRoutine;
    private Coroutine reloadRoutine;

    private Animator animator;
    private int hashShow;

    private AudioSource audioSource;

    // 총알이 마지막으로 발사된 시간을 기록한다(연타 가속 방지)
    private float lastFireTime;

    private void InitTableData()
    {
        firePeriod = new WaitForSeconds(DataTable.TimeBetFire);
        lastFireTime = Time.time;
        magazineRemain = DataTable.MagCapacity;
        bulletStock = DataTable.BulletStock;
    }

    public void UseBegin()
    {
        fireRoutine = StartCoroutine(Fire());
    }

    public void UseEnd()
    {
        if (fireRoutine != null)
        {
            StopCoroutine(fireRoutine);
        }
    }

    /// <summary>
    /// 총을 드러내고 숨기는 애니메이션을 재생합니다
    /// </summary>
    /// <param name="show">드러낸다면 true</param>
    public void ShowAnimation(bool show)
    {
        animator.SetBool(hashShow, show);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        hashShow = Animator.StringToHash("Show");
        audioSource = GetComponent<AudioSource>();

        InitTableData();
    }

    private IEnumerator Fire()
    {
        // 연타 가속 방지
        float canFireTime = lastFireTime + DataTable.TimeBetFire;
        if (canFireTime > Time.time)
        {
            yield return new WaitForSeconds(canFireTime - Time.time);
        }

        while (CurrentState == State.Ready)
        {
            lastFireTime = Time.time;
            Shot();
            OnShot?.Invoke();
            yield return firePeriod;
        }

        if (CurrentState == State.Empty)
        {
            reloadRoutine = StartCoroutine(ReloadRoutine());
        }
    }

    protected virtual void Shot()
    {
        // 트레일 등 이펙트를 그리기 위해 탄알이 맞은 곳을 저장
        Vector3 hitPosition = Vector3.zero;

        Debug.DrawRay(ShotPosition, DataTable.Range * ShotDircetion, Color.yellow, 0.1f);

        // 레이캐스트(시작 지점, 방향, 충돌 정보 컨테이너, 사정거리)
        if (Physics.Raycast(ShotPosition, ShotDircetion, out RaycastHit hit, DataTable.Range, DataTable.layerMask))
        {
            // 레이 적중시

            // 충돌한 상대방으로부터 IDamageable 오브젝트 가져오기 시도
            if (hit.rigidbody != null && hit.rigidbody.TryGetComponent(out IDamageable target))
            {
                target.Damaged(AttackPoint, 0);
                // 레이가 충돌한 위치 저장
                hitPosition = hit.point;
            }
        }
        else
        {
            // 레이가 다른 물체와 충돌하지 않았다면
            // 탄알이 최대 사정거리까지 날아갔을 때의 위치를 line 출력 종료 위치로 사용
            hitPosition = ShotPosition + ShotDircetion * DataTable.Range;

        }

        // 적중 여부와 무관히 발생하는 처리

        // 발사 이펙트 재생 시작
        StartCoroutine(ShotEffect(hitPosition));

        // 탄창 크기가 음수(무한 탄창)이라면 잔탄 검사 생략
        if (MagazineCapacity < 0)
            return;

        // 탄창 처리
        magazineRemain--;
        if (magazineRemain <= 0)
        {
            CurrentState = State.Empty;
        }

        Debug.Log($"잔탄 {magazineRemain}");
    }

    protected void StartEffect(Vector3 hitPosition) => StartCoroutine(ShotEffect(hitPosition));

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
        audioSource.PlayOneShot(DataTable.ShotClip, DataTable.ShotVolumeScale);
        if (muzzleParticle != null)
        {
            muzzleParticle.Play();
        }

        // TODO: 이펙트 처리

        Debug.Log("발사 이펙트 출력 필요");
        yield break;
    }

    /// <summary>
    /// 플레이어가 호출 가능한 Reload<br/>
    /// 재장전이 가능한 상태였다면 재장전 진입
    /// </summary>
    /// <returns>재장전 시작이 가능한 상태였는지 여부</returns>
    public bool Reload()
    {
        if (DataTable.MagCapacity <= 0
            || CurrentState == State.Reloading
            || magazineRemain >= DataTable.MagCapacity
            || bulletStock == 0)
        {
            // 탄창 크기 제한이 없거나
            // 이미 재장전 중이거나 남은 탄알이 없거나
            // 탄창에 탄알이 이미 가득 찬 경우  재장전할 수 없음
            return false;
        }

        // 재장전 처리 시작
        reloadRoutine = StartCoroutine(ReloadRoutine());
        return true;
    }

    private IEnumerator ReloadRoutine()
    {
        // 현재 상태를 재장전 중 장태로 전환
        CurrentState = State.Reloading;

        audioSource.PlayOneShot(DataTable.ReloadClip, DataTable.ReloadVolumeScale);
        ShowAnimation(false);

        // 재장전 소요 시간만큼 처리 쉬기
        yield return new WaitForSeconds(DataTable.ReloadTime);

        if (DataTable.BulletStock < 0)
        {
            magazineRemain = DataTable.MagCapacity;
        }
        else
        {
            // fillBullet: 실제로 장전될 양
            int fillBullet = DataTable.MagCapacity - magazineRemain;
            if (fillBullet > bulletStock)
                fillBullet = bulletStock;

            magazineRemain += fillBullet;
            bulletStock -= fillBullet;
        }


        ShowAnimation(true);

        // 총의 현재 상태를 발사 준비된 상태로 변경
        CurrentState = State.Ready;
    }
}
