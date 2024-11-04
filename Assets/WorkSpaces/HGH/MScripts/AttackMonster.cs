using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AttackMonster : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] protected PlayerModel playerModel;
    [SerializeField] protected MonsterModel monsterModel;
    [SerializeField] protected Animator monsterAni;

    [Header("Property")]
    [SerializeField] float rayDistance;

    public IDamageable player;
    private SphereCollider attackTrigger;
    private AudioSource audioSource;

    private void Awake()
    {
        monsterModel = GetComponent<MonsterModel>();
        attackTrigger = GetComponent<SphereCollider>();
        if (monsterAni == null)
        {
            monsterAni = GetComponent<Animator>();
        }
        audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
        playerModel = GameManager.Instance.GetPlayerModel();
        monsterModel.OnInit += Init;
    }

    private void OnDestroy()
    {
        monsterModel.OnInit -= Init;
    }

    private void Init()
    {
        attackTrigger.radius = monsterModel.DataTable.AttackRange;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // 코루틴이 null 이라면
        if (startMonsterAttackRoutine == null)
        {
            // 충돌체와 부딪힌 오브젝트의 태그가 Player라면
            if (other.gameObject.CompareTag("PlayerCollider"))
            {
                // 몬스터의 근접공격 코루틴을 시작시켜라
                player = other.attachedRigidbody.GetComponent<IDamageable>();
                
                startMonsterAttackRoutine = StartCoroutine(MonsterAutoAttack());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 코루틴이 null 이 아니라면
        if (other.gameObject.CompareTag("Player"))
        {
            if (startMonsterAttackRoutine != null)
            {
                // 코루틴 정지시키고
                StopCoroutine(startMonsterAttackRoutine);

                // 코루틴 비워라
                startMonsterAttackRoutine = null;
            }
        }
    }

    public virtual void AttackToPlayer()
    {
        // 몬스터 애니메이션에서 공격모션 재생 트리거
        monsterAni.SetTrigger("StartAttack");

        // 개발도중 Debug.Log 확인
        Debug.Log($"{monsterModel.MonsterAP}만큼의 몬스터 공격력으로 플레이어를 공격했다");
        if (player == null || monsterModel == null)
        {
            ;
        }
        // playerModel의 Hp를 monsterModel의 공격력만큼 감소
        player.Damaged(monsterModel.MonsterAP, DamageType.DEFAULT_MELEE_ATTACK);

        // 개발도중 Debug.Log 확인
        Debug.Log($"플레이어는 {playerModel.Hp}만큼 HP가 남았다");
    }

    protected Coroutine startMonsterAttackRoutine;

    protected virtual IEnumerator MonsterAutoAttack()
    {
        WaitForSeconds period = new WaitForSeconds(monsterModel.DataTable.AttackPeriod);
        while (true)
        {
            // 몬스터 공격 사운드
            if (monsterModel.DataTable.AttackClip != null)
            {
                audioSource.PlayOneShot(monsterModel.DataTable.AttackClip);
            }
            
            // 몬스터 공격시작
            AttackToPlayer();
            // 몬스터 공격 딜레이 1초 -> 테이블에서 가져오기
            yield return period;
        }
    }
}
