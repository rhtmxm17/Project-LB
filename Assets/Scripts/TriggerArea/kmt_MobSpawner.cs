using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kmt_MobSpawner : MonoBehaviour
{
    [SerializeField]
    float spawnDelay;
    [SerializeField]
    int spawnCount;

    [SerializeField]
    TestMonster spawnType;

    //todo : 작성중이신 몬스터 타입으로 바꿔 작성하기.
    TestMonster[] monsterPool;
    WaitForSeconds spawnTime;

    private void Awake()
    {

        spawnTime = new WaitForSeconds(spawnDelay);

        monsterPool = new TestMonster[spawnCount];

    }

    private void Start()
    {
        for (int i = 0; i < monsterPool.Length; i++)
        {
            monsterPool[i] = Instantiate(spawnType, transform.position, Quaternion.identity);
            monsterPool[i].gameObject.SetActive(false);
            monsterPool[i].transform.SetParent(transform);
        }
    }

    public void StartSpawning() {

        StartCoroutine(SpawningCo());

    }

    IEnumerator SpawningCo() {

        foreach (TestMonster monster in monsterPool) { 
        
            monster.gameObject.SetActive(true);

            //todo : 몬스터 추적모드로 바꾸는 함수를 받아서 사용.
            monster.ChangeToActive();

            yield return spawnTime;

        }

    }

}
