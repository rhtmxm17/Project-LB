using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    protected TestMonster[] spawnType;

    [Header("Spawn Random Monster Type In Arr")]
    [SerializeField]
    protected bool randomTypeSpawn;

    [Header("End Spawning Callback Event")]
    [SerializeField]
    protected UnityEvent EndSpawnEvent;

    //todo : 작성중이신 몬스터 타입으로 바꿔 작성하기.
    protected TestMonster[] monsterPool;
    protected WaitForSeconds spawnTime;

    protected virtual void Awake()
    {

        spawnTime = new WaitForSeconds(spawnDelay);
        monsterPool = new TestMonster[spawnCount];

    }

    protected virtual void Start()
    {
        int spawnIdx;

        for (int i = 0; i < monsterPool.Length; i++)
        {
            spawnIdx = randomTypeSpawn ? Random.Range(0, spawnType.Length) : 0;

            monsterPool[i] = Instantiate(spawnType[spawnIdx], transform.position, Quaternion.identity);
            monsterPool[i].gameObject.SetActive(false);
            monsterPool[i].transform.SetParent(waveMonsterParent);
        }
    }

    public void StartSpawning() {

        StartCoroutine(SpawningCo());

    }

    protected virtual IEnumerator SpawningCo() {

        foreach (TestMonster monster in monsterPool) { 
        
            monster.gameObject.SetActive(true);

            //todo : 몬스터 추적모드로 바꾸는 함수를 받아서 사용.
            monster.ChangeToActive();

            yield return spawnTime;

        }

        EndSpawnEvent?.Invoke();

    }

}
