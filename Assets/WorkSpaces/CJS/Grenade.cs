using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [field:SerializeField] public GrenadeData Data { get; set; }

    // 현재 최대 적중 가능 개체: 16
    private Collider[] explosionResults = new Collider[16];

    private void Explosion()
    {
        int hitted = Physics.OverlapSphereNonAlloc(transform.position, Data.ExplosionRange, explosionResults, Data.TargetLayer);

        for (int i = 0; i < hitted; i++)
        {
            // 피격 판정을 포함하는 유닛은 Rigidbody 필수
            // 자해 판정 허용(Data.TargetLayer를 통함)시 FRENDLY_GROUP_0을 통해 분류
            explosionResults[i].attachedRigidbody.GetComponent<IDamageable>().Damaged(Data.Damage, DamageType.FRENDLY_GROUP_0);
        }
    }
}
