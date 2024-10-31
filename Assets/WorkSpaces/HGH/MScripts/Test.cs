using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] MonsterTakenDamage monsterTakenDamage;
    [SerializeField] Vector3 power;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 100, Color.red);
        if (Physics.Raycast(transform.position, transform.forward*100,out RaycastHit hit))
        {
            if (hit.collider.gameObject.name == "Zombie")
            {
                monsterTakenDamage.MosterTakenKnockBack(transform.forward * 10f);
            }
        }
    }
}
