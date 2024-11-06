using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : GunBase
{
    private Collider[] hitColliders = new Collider[32];

    protected override void Shot()
    {
        // 트레일 등 이펙트를 그리기 위해 피격대상이 맞은 곳을 저장
        Vector3 hitPosition = Vector3.zero;

        int hitted = Physics.OverlapSphereNonAlloc(ShotPosition, DataTable.Range, hitColliders, DataTable.layerMask);
        
        for (int i = 0; i < hitted; i++)
        {
            if (hitColliders[i].attachedRigidbody != null && hitColliders[i].attachedRigidbody.TryGetComponent(out IDamageable target))
            {
                target.Damaged(AttackPoint, 0);
                hitPosition = hitColliders[i].transform.position;
            }
        }

        // 발사 이펙트 재생 시작
        StartEffect(hitPosition);
        SwingAnimation();

        // 탄창 크기가 음수(무한 탄창)이라면 잔탄 검사 생략
        if (MagazineCapacity < 0)
            return;

        // 탄창 처리
        MagazineRemain--;
        if (MagazineRemain <= 0)
        {
            CurrentState = State.Empty;
        }

        Debug.Log($"잔탄 {MagazineRemain}");
    }
}
