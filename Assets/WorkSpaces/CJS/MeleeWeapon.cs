using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : GunBase
{
    protected override void Shot()
    {
        // 트레일 등 이펙트를 그리기 위해 피격대상이 맞은 곳을 저장
        Vector3 hitPosition = Vector3.zero;

        Collider[] hitColliders = Physics.OverlapSphere(ShotPosition, DataTable.range); 
        int i = 0;
        while (i < hitColliders.Length) // hit Colliders(충돌체들) 의 Length(길이) 
        {
            if (hitColliders[i].attachedRigidbody != null && hitColliders[i].attachedRigidbody.TryGetComponent(out IDamageable target))
            {
                target.Damaged(DataTable.damage + DataTable.damageGrowth * GunLevel, 0);
                hitPosition = hitColliders[i].transform.position;
            }

            i++;
        }

        // 발사 이펙트 재생 시작
        StartEffect(hitPosition);
    }
}
