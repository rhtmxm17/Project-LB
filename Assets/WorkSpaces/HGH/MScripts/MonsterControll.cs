using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;

public class MonsterControll : MonoBehaviour
{
    [Header("Model")]
    [SerializeField] MonsterModel monsterModel;
    //[SerializeField] PlayerModel playerModel;

    [Header("Property")]
    public int variableHP;
    public int variableAP;

    private void Awake()
    {
        monsterModel = GetComponent<MonsterModel>();
        // 몬스터의 최대HP
        monsterModel.MonsterHP = 100;
        // 몬스터의 현재HP, 공격받고 HP가 줄어드는건 이 항목이며
        // 죽는 것도 MonsterCurHP 가 0이 되어야 죽는다
        monsterModel.MonsterCurHP = 100;
        // 몬스터의 AP
        monsterModel.MonsterAP = 10;
    }

    private void Start()
    {
        PlayerModel playerModel = GameManager.Instance.GetPlayerModel();
    }

    private void Update()
    {
        MonsterStatus();
    }

    public void MonsterStatus()
    {
        // 몬스터의 HP를 variableHP를 조정하여 변동 가능
        monsterModel.MonsterCurHP = variableHP;

        // 몬스터의 AP를 variableAP를 조정하여 변동 가능
        monsterModel.MonsterAP = variableAP;
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
