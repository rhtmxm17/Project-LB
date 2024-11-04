using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectorMonster : MonsterTakenDamage
{
    [SerializeField] MonsterData monsterDataSO;
    [SerializeField] ParticleSystem particleSys;

    protected override void MonsterDead()
    {
        if (monsterModel.MonsterCurHP <= 0)
        {


            MonsterModel instance = Instantiate(monsterDataSO.MonsterPrefab, transform.position, Quaternion.identity);
            instance.DataTable = monsterDataSO;
            instance.Init();
            instance.transform.SetParent(transform.parent);

            MonsterTraceToPlayer chase = instance.GetComponent<MonsterTraceToPlayer>();
            chase.ChaseOn();

            instance.GetComponent<MonsterTakenDamage>().OnDeadEvent += transform.parent.GetComponent<WaveManager>().CheckWaveIsClear;

            particleSys.Play();
            monsterAni.SetTrigger("DeadTrigger");
            // 몬스터 사망시
            transform.SetParent(null);
            OnDeadEvent?.Invoke();
            Destroy(gameObject, 1f);

        }
    }
}
