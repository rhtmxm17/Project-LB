using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreepWalk : AttackMonster
{

    public override void AttackToPlayer()
    {
        base.AttackToPlayer();

        int rand = Random.Range(0, 2);

        if (rand == 0)
        {
            monsterAni.SetTrigger("Walk");
        }
        else
        {
            monsterAni.SetTrigger("Crawl");
        }

    }

}
