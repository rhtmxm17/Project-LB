using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Timers;
using TMPro;
using UnityEngine;

public class UpgradeSystem : MonoBehaviour
{

    InventorySystem inventory;
    PlayerData playerData;
    ItemDataTableSO dataTable;

    InventoryEquableItemSO curItem = null;

    [SerializeField]
    InventoryItemSlot slot;
    [SerializeField]
    TextMeshProUGUI ReqGearNumText;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindWithTag("Inventory").GetComponent<InventorySystem>();
        playerData = GameManager.Instance.GetPlayerData();
        dataTable = GameManager.Instance.GetItemDataTable();
        CloseWindow();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            inventory.AddFood(20);
            inventory.AddGear(20);
        }
    }

    /// <summary>
    /// 현재 선택된 아이템을 교체
    /// </summary>
    /// <param name="newItem">교체할 아이템의 so 데이터</param>
    public void SetFocusedItem(InventoryItemSO newItem)
    {


        if (!(newItem is InventoryEquableItemSO))
        {
            Debug.Log("강화불가능!");
            return;
        }

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
        gameObject.SetActive(false);
    }
}
