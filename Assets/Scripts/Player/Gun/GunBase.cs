using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GunBase : MonoBehaviour, IUseable
{
    public enum State
    {
        Ready, // 발사 준비됨
        Empty, // 탄창이 빔
        Reloading // 재장전 중
    }

    public State CurrentState { get; private set; } // 현재 총의 상태

    [SerializeField] Transform muzzleTransform;
    [SerializeField] Transform weaponCamera;
    [field: SerializeField] public GunData DataTable { get; set; }

    public event UnityAction OnShot;
    public int MagazineCapacity => DataTable.magCapacity;
    public int MagazineRemain => magazineRemain;

    private int magazineRemain;
    private YieldInstruction firePeriod;

    private Coroutine fireRoutine;
    private Coroutine reloadRoutine;

    // 총알이 마지막으로 발사된 시간을 기록한다(연타 가속 방지)
    private float lastFireTime;

    private void Awake()
    {
        firePeriod = new WaitForSeconds(DataTable.timeBetFire);
        lastFireTime = Time.time;
        magazineRemain = DataTable.magCapacity;
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

    private IEnumerator Fire()
    {
        // 연타 가속 방지
        float canFireTime = lastFireTime + DataTable.timeBetFire;
        if (canFireTime > Time.time)
        {
            yield return new WaitForSeconds(canFireTime - Time.time);
        }

        while (CurrentState == State.Ready)
        {
            lastFireTime = Time.time;
            Shot();
            yield return firePeriod;
        }

        if (CurrentState == State.Empty)
        {
            reloadRoutine = StartCoroutine(ReloadRoutine());
        }
    }

    private void Shot()
    {
        OnShot?.Invoke();

        // 트레일 등 이펙트를 그리기 위해 탄알이 맞은 곳을 저장
        Vector3 hitPosition = Vector3.zero;

        Vector3 shotPosition;
        Vector3 shotDirection;
        if (weaponCamera == null)
        {
            shotPosition = muzzleTransform.position;
            shotDirection = muzzleTransform.forward;
        }
        else
        {
            // 무기 전용 카메라 사용시 실제 총구 대신 플레이어 눈에 보이는 총기 위치에서 발사
            shotPosition = Camera.main.transform.TransformPoint(weaponCamera.InverseTransformPoint(muzzleTransform.position));
            shotDirection = Camera.main.transform.TransformDirection(weaponCamera.InverseTransformDirection(muzzleTransform.forward));
        }
        Debug.DrawRay(shotPosition, DataTable.range * shotDirection, Color.yellow, 0.1f);

        // 레이캐스트(시작 지점, 방향, 충돌 정보 컨테이너, 사정거리)
        if (Physics.Raycast(shotPosition, shotDirection, out RaycastHit hit, DataTable.range, DataTable.layerMask))
        {
            // 레이 적중시

            // 충돌한 상대방으로부터 IDamageable 오브젝트 가져오기 시도
            if (hit.rigidbody != null && hit.rigidbody.TryGetComponent(out IDamageable target))
            {
                target.Damaged(DataTable.damage, 0);
                // 레이가 충돌한 위치 저장
                hitPosition = hit.point;
            }

            Debug.Log($"적중 대상: {hit.collider.name}");
        }
        else
        {
            // 레이가 다른 물체와 충돌하지 않았다면
            // 탄알이 최대 사정거리까지 날아갔을 때의 위치를 line 출력 종료 위치로 사용
            hitPosition = shotPosition + shotDirection * DataTable.range;

            Debug.Log($"적중 대상 없음");
        }

        // 적중 여부와 무관히 발생하는 처리

        // 발사 이펙트 재생 시작
        StartCoroutine(ShotEffect(hitPosition));

        // 탄창 처리
        magazineRemain--;
        if (magazineRemain <= 0)
        {
            CurrentState = State.Empty;
        }

    }

    private IEnumerator ShotEffect(Vector3 hitPosition)
    {
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
        if (CurrentState == State.Reloading || magazineRemain >= DataTable.magCapacity)
        {
            // 이미 재장전 중이거나 남은 탄알이 없거나
            // 탄창에 탄알이 이미 가득 찬 경우  재장전할 수 없음
            return false;
        }

        // 재장전 처리 시작
        StartCoroutine(ReloadRoutine());
        return true;
    }

    private IEnumerator ReloadRoutine()
    {
        // 현재 상태를 재장전 중 장태로 전환
        CurrentState = State.Reloading;

        // TODO: 재장전 소리 재생
        Debug.Log("재장전 사운드 출력 필요");

        // 재장전 소요 시간만큼 처리 쉬기
        yield return new WaitForSeconds(DataTable.reloadTime);

        magazineRemain = DataTable.magCapacity;

        // 총의 현재 상태를 발사 준비된 상태로 변경
        CurrentState = State.Ready;
    }
}
