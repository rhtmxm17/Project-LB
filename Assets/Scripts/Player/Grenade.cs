using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Grenade : MonoBehaviour
{
    [field:SerializeField] public GrenadeData Data { get; set; }

    [field:SerializeField] public GameObject GrenadeModel { get; set; }
    [field:SerializeField] public ParticleSystem ExplosionEffect { get; set; }

    // 현재 최대 적중 가능 개체: 32
    private Collider[] explosionResults = new Collider[32];

    AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// 지정된 시간 도달 후 폭발 및 소멸 루틴
    /// </summary>
    /// <param name="timer">지정 시간초</param>
    public IEnumerator ReadyExplosion(float timer)
    {
        yield return new WaitForSeconds(timer);

        Explosion();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Data.ExplosionRange);
    }

    private void Explosion()
    {
        int hitted = Physics.OverlapSphereNonAlloc(transform.position, Data.ExplosionRange, explosionResults, Data.TargetLayer);

        for (int i = 0; i < hitted; i++)
        {
            // 플레이어의 CharacterController가 감지된 경우 무시
            // 자해피해는 PlayerCollider 태그
            if (explosionResults[i].CompareTag("Player"))
                continue;

            // 피격 판정을 포함하는 유닛은 Rigidbody 필수
            // 자해 판정 허용(Data.TargetLayer를 통함)시 FRENDLY_GROUP_0을 통해 분류
            explosionResults[i].attachedRigidbody.GetComponent<IDamageable>().Damaged(Data.Damage, DamageType.FRENDLY_GROUP_0);
        }

        audioSource.Play();
        GrenadeModel.SetActive(false);
        ExplosionEffect.transform.SetParent(null);
        ExplosionEffect.Play();
        Destroy(gameObject, 4.5f);
    }
}
