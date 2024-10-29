using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MonsterModel : MonoBehaviour
{
    [SerializeField] int monsterHp;
    [SerializeField] int monsterCurHp;
    [SerializeField] int monsterAp;

    public int MonsterHP { get => monsterHp;}
    public int MonsterCurHP { get => monsterCurHp; set { monsterCurHp = value; OnMonsterHPChanged?.Invoke(monsterCurHp); } }
    public int MonsterAP { get { return monsterAp; } set { monsterAp = value; } }

    public UnityAction<int> OnMonsterHPChanged;
}
