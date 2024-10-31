using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class kmt_MobSpawner : MonoBehaviour
{

    [Header("Wave Monsters Parent")]
    [SerializeField]
    Transform waveMonsterParent;

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

    //todo : 작성중이신 몬스터 타입으로 바꿔 작성하기.
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

        for (int i = 0; i < monsterPool.Length; i++)
        {
            spawnIdx = randomTypeSpawn ? Random.Range(0, spawnType.Length) : 0;

            MonsterModel instance = Instantiate(spawnType[spawnIdx].MonsterPrefab, transform.position, Quaternion.identity);
            instance.DataTable = spawnType[spawnIdx];
            instance.Init();
            instance.gameObject.SetActive(false);
            instance.transform.SetParent(waveMonsterParent);

            monsterPool[i] = instance.GetComponent<MonsterTraceToPlayer>();
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
