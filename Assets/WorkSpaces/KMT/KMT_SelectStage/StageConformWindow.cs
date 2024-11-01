using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class StageConformWindow : MonoBehaviour
{
    [SerializeField]
    HotKeySystem hotKeySystem;
    [SerializeField]
    TextMeshProUGUI text;

    StageSceneManager stageSceneManager;

    StageData curData = null;

    private void Awake()
    {
        stageSceneManager = GameManager.Instance.GetStageSceneManager();
    }

    public void OpenWindow(StageData stageData)
    {
        gameObject.SetActive(true);
        curData = stageData;
        //todo : 이름으로 받아오기.
        text.text = $"ARE YOU SURE YOU WANT TO PLAY {curData.StageName}? ";
    }

    public void ConformButton()
    {
        ItemType baseWeapon = ItemType.NONE;
        ItemType meeleWeapon = ItemType.NONE;
        ItemType specialWeapon = ItemType.NONE;

        InventoryHotKeySlot slot = hotKeySystem.GetHotkeySlot(0);
        if (slot.Item != null)
        {
            baseWeapon = slot.Item.ItemType;
        }

        slot = hotKeySystem.GetHotkeySlot(1);
        if (slot.Item != null)
        {
            meeleWeapon = slot.Item.ItemType;
        }

        slot = hotKeySystem.GetHotkeySlot(4);
        if (slot.Item != null)
        {
            specialWeapon = slot.Item.ItemType;
        }

        //Debug.Log(baseWeapon + "/" + meeleWeapon + "/" + specialWeapon);

        stageSceneManager.EnterStage(baseWeapon, meeleWeapon, specialWeapon, curData);
    }

}
