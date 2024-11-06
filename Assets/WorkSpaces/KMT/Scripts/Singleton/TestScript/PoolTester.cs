using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolTester : MonoBehaviour
{

    List<PoolAble> l = new List<PoolAble>();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            l.Add(PoolManager.GetInstance().GetObj<PoolObject>());
            l[l.Count - 1].transform.SetParent(transform);
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            l[0].BackToPool();
            l.RemoveAt(0);
        }
    }
}
