using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoulMonster : MonsterTakenDamage
{
    [SerializeField] MonsterData monsterDataSO;

    protected override void MonsterDead()
    {
        if (monsterModel.MonsterCurHP <= 0)
        {

            for (int i = 0; i < 3; i++)
            {
                MonsterModel instance = Instantiate(monsterDataSO.MonsterPrefab, transform.position, Quaternion.identity);
                instance.DataTable = monsterDataSO;
                instance.Init();
                instance.transform.SetParent(transform.parent);

                MonsterTraceToPlayer chase = instance.GetComponent<MonsterTraceToPlayer>();
                chase.ChaseOn();

                instance.GetComponent<MonsterTakenDamage>().OnDeadEvent += transform.parent.GetComponent<WaveManager>().CheckWaveIsClear;

            }
            monsterAni.SetTrigger("DeadTrigger");
            // 몬스터 사망시
            transform.SetParent(null);
            OnDeadEvent?.Invoke();
            Destroy(gameObject, 1f);

        }
    }

}
