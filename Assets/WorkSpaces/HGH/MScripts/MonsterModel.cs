using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterModel : MonoBehaviour
{
    [SerializeField] int monsterHp;

    public int MonsterHP { get => monsterHp; set { monsterHp = value; OnMonsterHPChange(); } }

    public UnityAction<int> OnMonsterHPChanged;
}
