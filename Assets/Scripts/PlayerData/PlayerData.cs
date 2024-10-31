using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static StageConditionDialogueSO;
using JetBrains.Annotations;
using Unity.VisualScripting.FullSerializer;

[Serializable]
public class ItemData 
{

    [SerializeField]
    public int invenIdx;
    [SerializeField]
    public ItemType itemType;
    [SerializeField]
    public int count;

    [SerializeField]
    public int WeaponLevel;

}

//todo : 아이템 데이터테이블 작성 필요. => 다른곳에 static dic형식으로 작성하면 될 듯.

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

    public List<ItemData> inventoryData;

    [SerializeField] int food;
    [SerializeField] int gear;

    public int MaxHP { get { return maxHP; } private set { } }
    public int Level { get { return level; } private set { } }
    public int Exp { get { return exp; } private set { } }
    public List<ItemData> InventoryData {  get { return inventoryData; } private set { } }
    public int Food { get { return food; } private set { } }
    public int Gear { get { return gear; } private set { } }

    //불러올 데이터가 없을 때 또는 새로 데이터를 만들 때 호출
    public PlayerData() {

        maxHP = 100;
        hpIncreaseAmount = 20;

        level = 1;
        exp = 0;

        reqExpArr = new int[] { 50, 100, 150, 200, 300, 300 };

        stageClearCntArr = new int[] { 0, 0, 0, 0 };
        hiddenArr = new bool[] { false, false, false };
        storyArr = new bool[] { false, false, false, false, false };

        inventoryData = new List<ItemData>(21);
        InitInventory();

        food = 0;
        gear = 0;

    }

    void InitInventory() 
    {
        int itemTypeCnt = (int)ItemType.SIZE;

        for (int i = 0; i < itemTypeCnt; i++)
        {

            ItemData tmp;

            tmp = new ItemData()
            {
                itemType = (ItemType)i,
                invenIdx = 0,
                count = 0,

                //필요한 경우에만 사용하기.
                WeaponLevel = 0
            };

            inventoryData.Add(tmp);
        }

    }


    /// <summary>
    /// 아이템 타입을 받아 플레이어가 가지고있는 해당 아이템 정보를 반환.
    /// </summary>
    /// <param name="type">찾을 아이템 타입</param>
    /// <returns>찾은 아이템 정보, 없으면 null 반환.</returns>
    public ItemData GetItemData(ItemType type)
    {
        ItemData ret = null;

        foreach (ItemData item in inventoryData)
        {
            if (item.itemType == type)
            {
                ret = item;
                break;
            }
        }

        return ret;

    }


    /// <summary>
    /// 경험치 추가
    /// </summary>
    /// <param name="amount">획득할 경험치</param>
    /// <returns>레벨업을 한 경우 true반환, 아닌경우 false반환</returns>
    public bool AddExp(int amount) {

        //만랩인경우
        if (level >= reqExpArr.Length)
            return false;

        exp += amount;

        //레벨업
        if (exp >= reqExpArr[level - 1])
        {
            exp -= reqExpArr[level - 1];
            level++;
            maxHP += hpIncreaseAmount;
            return true;
        }

        return false;

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
