using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterControll : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] MonsterModel monsterModel;
    //[SerializeField] PlayerModel playerModel;


    private void Awake()
    {
        monsterModel = GetComponent<MonsterModel>();
        // 몬스터의 현재HP, 공격받고 HP가 줄어드는건 이 항목이며
        // 죽는 것도 MonsterCurHP 가 0이 되어야 죽는다
        monsterModel.MonsterCurHP = 100;
    }

    private void Start()
    {
        PlayerModel playerModel = GameManager.Instance.GetPlayerModel();
    }


    //private void OnEnable()
    //{
    //    monsterModel.OnMonsterHPChanged += UpdateMonsterHP;
    //}
    //
    //private void OnDisable()
    //{
    //    monsterModel.OnMonsterHPChanged -= UpdateMonsterHP;
    //}
    //
    //public void UpdateMonsterHP(int monsterHp)
    //{
    //
    //    // UI 부분: 몬스터의 HP체력바의 수치 변경
    //}
}
