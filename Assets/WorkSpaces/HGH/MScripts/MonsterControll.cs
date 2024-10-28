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

    private void Awake()
    {
        monsterModel = GetComponent<MonsterModel>();
        
    }

    private void Start()
    {
        PlayerModel playerModel = GameManager.Instance.GetPlayerModel();
    }

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
