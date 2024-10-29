using System.Collections;
using UnityEngine;

public class kmt_CubeAreaMobSpawner : kmt_MobSpawner
{

    Vector3 leftDown;
    Vector3 rightTop;

    Vector3 halfSize;

    [Header("Spawn Random Position In Area")]
    [SerializeField]
    bool isRandomSpawn;

    protected override void Awake()
    {
        base.Awake();

        halfSize = Vector3.one / 2.0f;

        leftDown = new Vector3(-halfSize.x, halfSize.y, -halfSize.z);
        rightTop = new Vector3(halfSize.x, halfSize.y, halfSize.z);

    }

    protected override void Start()
    {
        base.Start();
    }



    protected override IEnumerator SpawningCo()
    {

        Vector3 pos;

        foreach (MonsterTraceToPlayer monster in monsterPool)
        {

            if (isRandomSpawn)
            {
                pos = new Vector3(
                    Random.Range(-halfSize.x, halfSize.x),
                    halfSize.y,
                    Random.Range(-halfSize.z, halfSize.z)
                    );
            }
            else
            {
                pos = Vector3.zero;
            }

            monster.gameObject.SetActive(true);

            monster.transform.position = transform.TransformPoint(pos);

            //todo : 몬스터 추적모드로 바꾸는 함수를 받아서 사용.
            monster.ChaseOn();

            yield return spawnTime;

        }

        EndSpawnEvent?.Invoke();
    }

}
