using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tester : MonoBehaviour
{
    [SerializeField]
    InventoryItemSO itemSO;

    [SerializeField]
    int idx;

    [SerializeField]
    InventorySystem inven;

    [SerializeField]
    UpgradeSystem upgradeSystem;

    [SerializeField]
    ExchangeSystem exchangeSystem;

    [SerializeField]
    PlayerInfoSystem playerInfoSystem;

    [SerializeField]
    int foodAmount;

    public void AddButton()
    { 
        inven.AddItem(itemSO, idx);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inven.OpenWindow();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            upgradeSystem.OpenWindow();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            exchangeSystem.OpenWindow();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerInfoSystem.OpenWindow();
        }


    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;

    }

    public void RemoveButton()
    {
        inven.RemoveItem(idx);
    }

    public void AddFood()
    { 
        inven.AddFood(foodAmount);
    }

    public void UseFood()
    { 
        inven.UseFood(foodAmount);

        PlayerData da = GameManager.Instance.GetPlayerData();

        ItemData gundata = da.GetItemData(ItemType.FAMAS);

        InventoryItemSO d = GameManager.Instance.GetItemDataTable().GetItemDataSO(ItemType.FAMAS);

        if (gundata.WeaponLevel >= ((InventoryEquableItemSO)d).MaxUpgradeLevel)
        {
            Debug.Log("풀강!");
        }
        else
        {

            gundata.WeaponLevel++;
        }

    }
}
