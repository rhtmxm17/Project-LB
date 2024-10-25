using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterModel : MonoBehaviour
{
    [SerializeField] int monsterHp;
    [SerializeField] int monsterAp;

    public int MonsterHP { get => monsterHp; set { monsterHp = value; OnMonsterHPChanged?.Invoke(monsterHp); } }
    public int MonsterAP { get { return monsterAp; } set { monsterAp = value; } }

    public UnityAction<int> OnMonsterHPChanged;
}
