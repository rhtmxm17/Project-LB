using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static StageConditionDialogueSO;

[Serializable]
public class PlayerData
{
    [SerializeField] int maxHP;
    [SerializeField] int hpIncreaseAmount;

    [SerializeField] int level;
    [SerializeField] int exp;


    [SerializeField] int[] reqExpArr;

    public int[] stageClearCntArr;
    public bool[] hiddenArr;
    public bool[] storyArr;

    [SerializeField] int food;
    [SerializeField] int gear;

    public int MaxHP { get { return maxHP; } private set { } }
    public int Level { get { return level; } private set { } }
    public int Exp { get { return exp; } private set { } }
    public int Food { get { return food; } private set { } }
    public int Gear { get { return gear; } private set { } }

    public PlayerData() {

        maxHP = 100;
        hpIncreaseAmount = 20;

        level = 1;
        exp = 0;

        //todo : 벨런스 조정
        reqExpArr = new int[] { 100, 200, 300, 400, 500, 600 };

        stageClearCntArr = new int[] { 0, 0, 0, 0 };
        hiddenArr = new bool[] { false, false, false };
        storyArr = new bool[] { false, false, false, false, false };

        food = 0;
        gear = 0;

    }

    /// <summary>
    /// 경험치 추가
    /// </summary>
    /// <param name="amount">추가할 양</param>
    public void AddExp(int amount) {

        //만랩인경우
        if (level >= reqExpArr.Length)
            return;

        exp += amount;

        //레벨업
        if (exp >= reqExpArr[level - 1])
        {
            exp -= reqExpArr[level - 1];
            level++;
            maxHP += hpIncreaseAmount;
        }

    }


    /// <summary>
    /// 음식 추가
    /// </summary>
    /// <param name="amount">추가할 양</param>
    public void AddFood(int amount)
    {
        food += amount;
    }
    /// <summary>
    /// 음식 사용
    /// </summary>
    /// <param name="amount">사용할 양</param>
    /// <returns>음식이 충분하면 사용 후 true, 부족하면 false 반환</returns>
    public bool UseFood(int amount)
    {
        //음식 부족
        if (food - amount < 0) return false;

        food -= amount;
        return true;

    }


    /// <summary>
    /// 기어 추가
    /// </summary>
    /// <param name="amount">추가할 양</param>
    public void AddGear(int amount)
    { 
        gear += amount;
    }
    /// <summary>
    /// 기어 사용
    /// </summary>
    /// <param name="amount">사용할 양</param>
    /// <returns>기어가 충분하면 사용 후 true, 부족하면 false 반환</returns>
    public bool UseGear(int amount)
    {
        //기어 부족
        if (gear - amount < 0) return false;

        gear -= amount;
        return true;

    }

}
