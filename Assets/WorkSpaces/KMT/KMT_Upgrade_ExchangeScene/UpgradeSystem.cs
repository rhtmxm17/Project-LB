using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeSystem : MonoBehaviour
{

    PlayerData playerData;
    ItemDataTableSO dataTable;

    InventoryEquableItemSO curItem = null;

    [Header("Sprites")]
    [SerializeField]
    Sprite gear;
    [SerializeField]
    Sprite bluePrint;

    [Header("Req Item Img")]
    [SerializeField]
    Image reqImg;

    [SerializeField]
    InventoryItemSlot slot;
    [SerializeField]
    TextMeshProUGUI ReqGearNumText;

    [Header("Inventory")]
    [SerializeField]
    InventorySystem inventory;


    // Start is called before the first frame update
    void Start()
    {
        playerData = GameManager.Instance.GetPlayerData();
        dataTable = GameManager.Instance.GetItemDataTable();
        CloseWindow();
    }


    /// <summary>
    /// 현재 선택된 아이템을 교체
    /// </summary>
    /// <param name="newItem">교체할 아이템의 so 데이터</param>
    public void SetFocusedItem(InventoryItemSO newItem)
    {

        if (newItem.ItemType == ItemType.SP_WEAPON1 ||
           newItem.ItemType == ItemType.SP_WEAPON2 ||
           newItem.ItemType == ItemType.SP_WEAPON3)
        {
            Debug.Log("레시피");
            ReqGearNumText.text = "1";
            reqImg.sprite = bluePrint;
            curItem = (InventoryEquableItemSO)newItem;
            return;
        }

        if (!(newItem is InventoryEquableItemSO))
        {
            Debug.Log("강화불가능!");
            return;
        }

        reqImg.sprite = gear;
        curItem = (InventoryEquableItemSO)newItem;

        ItemData data = playerData.GetItemData(newItem.ItemType);
        InventoryEquableItemSO item = (InventoryEquableItemSO)dataTable.GetItemDataSO(newItem.ItemType);

        if (data.WeaponLevel >= item.MaxUpgradeLevel)
        {
            Debug.LogWarning("최대레벨입니다!");
            ReqGearNumText.text = "Max";
        }
        else
        {
            ReqGearNumText.text = item.UpgradeReqGears[data.WeaponLevel].ToString();
        }
        
    }

    public void UpgradeButton()
    {

        if (curItem == null)
        {
            Debug.Log("선택된 아이템이 없음");
            return;
        }

        ItemData data = playerData.GetItemData(curItem.ItemType);
        InventoryEquableItemSO item = (InventoryEquableItemSO)dataTable.GetItemDataSO(curItem.ItemType);

        if (item.ItemType == ItemType.SP_WEAPON1 ||
           item.ItemType == ItemType.SP_WEAPON2 ||
           item.ItemType == ItemType.SP_WEAPON3)
        {

            if (data.count > 0)
            {
                Debug.Log("이미 소지하고 있는 히든 아이템");
                return;
            }

            if (item.ItemType == ItemType.SP_WEAPON1 && 
                playerData.GetItemData(ItemType.BLUEPRINT_SP1).count > 0)
            {
                playerData.GetItemData(ItemType.SP_WEAPON1).count = 1;
                playerData.GetItemData(ItemType.SP_WEAPON1).invenIdx = item.InventoryIdx;
                inventory.AddItem(item, item.InventoryIdx);
            }
            else if(item.ItemType == ItemType.SP_WEAPON2 &&
                playerData.GetItemData(ItemType.BLUEPRINT_SP2).count > 0)
            {
                playerData.GetItemData(ItemType.SP_WEAPON2).count = 1;
                playerData.GetItemData(ItemType.SP_WEAPON2).invenIdx = item.InventoryIdx;
                inventory.AddItem(item, item.InventoryIdx);

            }
            else if (item.ItemType == ItemType.SP_WEAPON3 &&
                playerData.GetItemData(ItemType.BLUEPRINT_SP3).count > 0)
            {
                playerData.GetItemData(ItemType.SP_WEAPON3).count = 1;
                playerData.GetItemData(ItemType.SP_WEAPON3).invenIdx = item.InventoryIdx;
                inventory.AddItem(item, item.InventoryIdx);
            }
            return;

        }

        if (curItem != null)
        {//강화 시도 가능 

            if (data.WeaponLevel >= item.MaxUpgradeLevel)
            {
                Debug.Log("최대레벨입니다!");
                return;
            }

            int reqGear = item.UpgradeReqGears[data.WeaponLevel];

            if (inventory.CurrentGear() >= reqGear)
            {//기어가 충분한지 확인.
                //레벨업 시킴
                data.WeaponLevel++;
                //다음 필요 기어 정보 바꿈
                SetFocusedItem(curItem);
                //기어 사용
                inventory.UseGear(reqGear);
            }
            else
            {
                Debug.Log("기어가 부족합니다!!");
            }

        }
        else
        {
            Debug.Log("선택된 아이템이 없습니다.");
        }

    }

    public void OpenWindow()
    {
        gameObject.SetActive(true);
    }

    public void CloseWindow()
    {
        curItem = null;
        slot.SetItem();
        ReqGearNumText.text = "0";
        reqImg.sprite = gear;
        gameObject.SetActive(false);
    }
}
