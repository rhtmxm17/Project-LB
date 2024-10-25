using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterControll : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] MonsterModel monsterModel;
    [SerializeField] PlayerModel playerModel;

    private void OnEnable()
    {
        monsterModel.OnMonsterHPChanged += UpdateMonsterHP;
    }

    private void OnDisable()
    {
        monsterModel.OnMonsterHPChanged -= UpdateMonsterHP;
    }

    public void UpdateMonsterHP(int monsterHp)
    {
        // UI 부분: 몬스터의 HP체력바의 수치 변경
    }
}
