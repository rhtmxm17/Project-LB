using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InventorySystem : MonoBehaviour
{
    [SerializeField]
    Transform itemSlotParent;

    [SerializeField]
    int slotCount;

    [SerializeField]
    GameObject ItemSlotPrefab;

    InventoryItemSlot[] itemSlotArr;

    [Header("Lower UI Text")]
    [SerializeField]
    TextMeshProUGUI foodText;
    [SerializeField]
    TextMeshProUGUI gearText;

    [Header("Info Windows")]
    [SerializeField]
    GameObject InfoWindow;
    [SerializeField]
    GameObject DetailInfoWindow;

    [SerializeField]
    PlayerCharacterControllerControl playerCharacterControllerControl;

    PlayerData playerData;
    ItemDataTableSO dataTable;

    private void Awake()
    {
        
        itemSlotArr = new InventoryItemSlot[slotCount];

        for (int i = 0; i < slotCount; i++)
        {
            itemSlotArr[i] =
                Instantiate(ItemSlotPrefab, itemSlotParent)
                .GetComponentInChildren<InventoryItemSlot>();

        }

        playerData = GameManager.Instance.GetPlayerData();
        dataTable = GameManager.Instance.GetItemDataTable();
        List<ItemData> itemDatas = playerData.inventoryData;

        for (int i = 0; i < itemDatas.Count; i++)
        {
            ItemData tmpData = itemDatas[i];

            //없으면 인벤토리에 배치하지 않음.
            if (tmpData.count <= 0)
                continue;

            InitItemSlot(dataTable.GetItemDataSO(tmpData.itemType), tmpData.invenIdx);

        }

        RefreshText();

    }

    private void Start()
    {
        gameObject.SetActive(false);
    }


    //todo : 아이템을 추가할 때 playerdata에 갯수 추가하는 작업 필요(max보다 많으면 기각)

    /// <summary>
    /// 아이템을 슬롯의 정보를 읽어온 정보로 초기 세팅함.
    /// </summary>
    /// <param name="item">저장할 아이템 so 정보</param>
    /// <param name="idx">저장할 슬롯 위치</param>
    public void InitItemSlot(InventoryItemSO item, int idx)
    {
        if (idx < 0 || idx >= itemSlotArr.Length)
        {
            Debug.LogError("아이템 창 범위를 벗어난 인덱스 값");
            return;
        }

        if (itemSlotArr[idx].Item != item && itemSlotArr[idx].Item != null)
        {
            Debug.LogError("다른 종류의 아이템이 이미 존재.");
            return;
        }

        itemSlotArr[idx].SetItem(item);


    }

    /// <summary>
    /// 인벤토리에 아이템을 추가.
    /// </summary>
    /// <param name="item">추가할 아이템 so 정보</param>
    /// <param name="idx">추가할 아이템 슬롯 위치</param>
    public void AddItem(InventoryItemSO item, int idx)
    {

        if (idx < 0 || idx >= itemSlotArr.Length)
        {
            Debug.LogError("아이템 창 범위를 벗어난 인덱스 값");
            return;
        }

        if (itemSlotArr[idx].Item != item && itemSlotArr[idx].Item != null)
        {
            Debug.LogError("다른 종류의 아이템이 이미 존재.");
            return;
        }


        ItemData tmpData = null;

        for (int i = 0; i < playerData.inventoryData.Count; i++)
        {

            if (item.ItemType == playerData.inventoryData[i].itemType)
            {
                tmpData = playerData.inventoryData[i];
                break;
            }

        }

        if (tmpData == null)
        {
            Debug.LogError("데이터테이블에 존재하지 않는 아이템. 인벤토리 매칭 오류");
            return;
        }

        //todo : 아이템 중첩 확인.
        //우선순위는 후순위(겹쳐지는 아이템이 없기 때문)

        //인벤토리에 아이템 세팅.
        itemSlotArr[idx].SetItem(item);

        tmpData.invenIdx = idx;
        tmpData.count = 1;

    }

    public void RemoveItem(int idx)
    {
        if (idx < 0 || idx >= itemSlotArr.Length)
        {
            Debug.LogError("아이템 창 범위를 벗어난 인덱스 값");
            return;
        }

        itemSlotArr[idx].SetItem();

    }


    /// <summary>
    /// 하단 아이템 텍스트 정보 갱신
    /// </summary>
    private void RefreshText()
    {
        foodText.text = playerData.Food.ToString();
        gearText.text = playerData.Gear.ToString();
    }

    public int CurrentFood()
    {
        return playerData.Food;
    }

    public bool UseFood(int amount)
    {
        if (playerData.UseFood(amount))
        {
            RefreshText();
            return true;
        }

        //음식 부족
        return false;
    }

    public void AddFood(int amount)
    {
        playerData.AddFood(amount);
        RefreshText();
    }


    public int CurrentGear()
    {
        return playerData.Gear;
    }

    public bool UseGear(int amount)
    {
        if (playerData.UseGear(amount))
        {
            RefreshText();
            return true;
        }

        //기어 부족
        return false;
    }

    public void AddGear(int amount)
    {
        playerData.AddGear(amount);
        RefreshText();
    }

    public void OpenWindow()
    {
        gameObject.SetActive(true);
        playerCharacterControllerControl.MouseLock(false);

    }
    public void CloseWindow()
    {
        gameObject.SetActive(false);
        InfoWindow.SetActive(false);
        DetailInfoWindow.SetActive(false);
        playerCharacterControllerControl.MouseLock(true);

    }

}
