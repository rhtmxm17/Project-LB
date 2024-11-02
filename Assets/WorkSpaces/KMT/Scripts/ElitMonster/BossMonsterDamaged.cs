using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossMonsterDamaged : MonsterTakenDamage
{

    int curWaveHP;
    int waveIdx;

    int[] waveHpArr;
    Action[] waveActionArr;
    NavMeshAgent nav;

    [SerializeField]
    BossMonsterRangeAttack rangeAttack;

    [Header("Minion Monsters Info")]
    [SerializeField]
    MonsterData normalMonsterData;

    [SerializeField]
    MonsterData[] elitMonsterArr;

    protected override void Awake()
    {
        base.Awake();

        waveHpArr = new int[]
        {
            15000 / 100 * 80,
            15000 / 100 * 60,
            15000 / 100 * 40
        };
        waveIdx = 0;

        waveActionArr = new Action[]
        {
            Wave1,
            Wave2,
            Wave3
        };

        curWaveHP = waveHpArr[waveIdx];

        nav = GetComponent<NavMeshAgent>();

    }

    public override void Damaged(int damage, DamageType type)
    {
        monsterAni.SetTrigger("TakenDamaged");
        // 몬스터 HP가 damage만큼 감소

        monsterModel.MonsterCurHP -= damage;

        Debug.Log("HP : " + monsterModel.MonsterCurHP);
        Debug.Log("HP : req" + curWaveHP);

        if (waveIdx < waveHpArr.Length && monsterModel.MonsterCurHP <= curWaveHP)
        {
            StartCoroutine(Crying());
            monsterAni.Play("Standing Taunt Battlecry");
            waveActionArr[waveIdx]?.Invoke();
            waveIdx++;
            if(waveIdx < waveHpArr.Length)
                curWaveHP = waveHpArr[waveIdx];
        }
        
        MonsterDead();
    }

    bool isDead = false;

    protected override void MonsterDead()
    {
        if(isDead) return;

        if (monsterModel.MonsterCurHP <= 0)
        {
            isDead = true;
            rangeAttack.IsCring = true;
            nav.speed = 0;

            monsterAni.SetTrigger("DeadTrigger");
            // 몬스터 사망시
            transform.SetParent(null);
            OnDeadEvent?.Invoke();
            Destroy(gameObject, 4f);
        }
    }


    IEnumerator Crying()
    {
        rangeAttack.IsCring = true;
        yield return new WaitForSeconds(2.8f);
        rangeAttack.IsCring = false;
    }

    void Wave1()
    {
        HashSet<int> randSet = new HashSet<int>();

        for (int i = 0; i < 20; i++)
        {
            SummonMonster(normalMonsterData);
        }

        while (randSet.Count < 3)
        {
            randSet.Add(UnityEngine.Random.Range(0, elitMonsterArr.Length));
        }

        foreach (int idx in randSet)
        { 
            SummonMonster(elitMonsterArr[idx]);
        }

    }

    void Wave2()
    {
        HashSet<int> randSet = new HashSet<int>();

        for (int i = 0; i < 25; i++)
        {
            SummonMonster(normalMonsterData);
        }

        while (randSet.Count < 4)
        {
            randSet.Add(UnityEngine.Random.Range(0, elitMonsterArr.Length));
        }

        foreach (int idx in randSet)
        {
            SummonMonster(elitMonsterArr[idx]);
        }
    }

    void Wave3()
    {
        for (int i = 0; i < 30; i++)
        {
            SummonMonster(normalMonsterData);
        }

        foreach (MonsterData data in elitMonsterArr)
        {
            SummonMonster(data);
        }
    }

    void SummonMonster(MonsterData monsterDataSO)
    {
        MonsterModel instance = Instantiate(monsterDataSO.MonsterPrefab, transform.position, Quaternion.identity);
        instance.DataTable = monsterDataSO;
        instance.Init();
        instance.transform.SetParent(transform.parent);

        MonsterTraceToPlayer chase = instance.GetComponent<MonsterTraceToPlayer>();
        chase.ChaseOn();

        instance.GetComponent<MonsterTakenDamage>().OnDeadEvent += transform.parent.GetComponent<WaveManager>().CheckWaveIsClear;
    }

}
