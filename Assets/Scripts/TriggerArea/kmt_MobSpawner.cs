using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class kmt_MobSpawner : MonoBehaviour
{

    [Header("Wave Monsters Parent")]
    [SerializeField]
    WaveManager waveMonsterParent;

    [Header("Spawn Delay and Count")]
    [SerializeField]
    protected float spawnDelay;
    [SerializeField]
    protected int spawnCount;

    [Header("Monster Types")]
    [SerializeField]
    protected MonsterData[] spawnType;

    [Header("Spawn Random Monster Type In Arr")]
    [SerializeField]
    protected bool randomTypeSpawn;

    [Header("End Spawning Callback Event")]
    [SerializeField]
    protected UnityEvent EndSpawnEvent;

    protected MonsterTraceToPlayer[] monsterPool;
    protected WaitForSeconds spawnTime;

    protected virtual void Awake()
    {
        spawnTime = new WaitForSeconds(spawnDelay);
        monsterPool = new MonsterTraceToPlayer[spawnCount];
    }

    protected virtual void Start()
    {
        int spawnIdx;

        if(waveMonsterParent == null)
        {
            Debug.LogWarning("웨이브 매니져가 연결되어있지 않습니다.");
            return;
        }

        for (int i = 0; i < monsterPool.Length; i++)
        {
            spawnIdx = randomTypeSpawn ? Random.Range(0, spawnType.Length) : 0;

            MonsterModel instance = Instantiate(spawnType[spawnIdx].MonsterPrefab, transform.position, Quaternion.identity);
            instance.DataTable = spawnType[spawnIdx];
            instance.Init();
            instance.gameObject.SetActive(false);
            instance.transform.SetParent(waveMonsterParent.transform);

            monsterPool[i] = instance.GetComponent<MonsterTraceToPlayer>();

            instance.GetComponent<MonsterTakenDamage>().OnDeadEvent += waveMonsterParent.CheckWaveIsClear;
        }
    }

    public void StartSpawning() {

        StartCoroutine(SpawningCo());

    }

    protected virtual IEnumerator SpawningCo() {

        foreach (MonsterTraceToPlayer monster in monsterPool) { 
        
            monster.gameObject.SetActive(true);

            //todo : 몬스터 추적모드로 바꾸는 함수를 받아서 사용.
            monster.ChaseOn();

            yield return spawnTime;

        }

        EndSpawnEvent?.Invoke();

    }

}
