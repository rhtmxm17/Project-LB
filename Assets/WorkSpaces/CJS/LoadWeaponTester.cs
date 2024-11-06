using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadWeaponTester : MonoBehaviour
{
    [SerializeField] StageData stage;
    [SerializeField] ItemType mainWeap = ItemType.AK47;
    [SerializeField] int mainWeapLevel = 0;
    [SerializeField] ItemType meleeWeap = ItemType.KNIFE;
    [SerializeField] ItemType specialWeap = ItemType.SP_WEAPON1;

    [ContextMenu("Test Start")]
    public void TestStart()
    {
        var playerData = GameManager.Instance.GetPlayerData();

        playerData.GetItemData(mainWeap).WeaponLevel = mainWeapLevel;

        GameManager.Instance.GetStageSceneManager().EnterStage(mainWeap, meleeWeap, specialWeap, stage);
    }
}
