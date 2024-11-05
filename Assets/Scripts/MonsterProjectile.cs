using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MonsterProjectile : MonoBehaviour
{
    [SerializeField] LayerMask targetLayer;
    [SerializeField] float shotForce;
    [SerializeField] StatusDebuff debuff;

    public int AttackPoint { get; set; }
    public Rigidbody Body => GetComponent<Rigidbody>();

    public void SetDestination(Vector3 position)
    {
        Vector3 dircetion = (position - transform.position).normalized;
        Body.AddForce(dircetion * shotForce, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        // 충돌 대상 레이어가 아니라면 무시
        // 프로젝트 셋팅에 따라 그 외에도 충돌하지 않는 레이어가 있을 수 있음 주의
        if (0 == (targetLayer & (1 << other.gameObject.layer)))
            return;

        // CharacterController 무시
        if (other is CharacterController)
            return;

        Rigidbody targetBody = other.attachedRigidbody;
        if (targetBody != null)
        {
            if (targetBody.TryGetComponent(out IDamageable target))
            {
                target.Damaged(AttackPoint, 0);
            }

            // 디버프 부여가 가능한 조건이라면 부여
            if (debuff != null && targetBody.TryGetComponent(out PlayerModel debuffable))
            {
                debuffable.AddDebuff(debuff);
            }
        }

        Destroy(gameObject);
    }
}
