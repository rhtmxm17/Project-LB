using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;

public class ExchangeSystem : MonoBehaviour
{

    InventorySystem inventory;
    PlayerData playerData;

    [SerializeField]
    InputPositiveNumber inputChecker;

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindWithTag("Inventory").GetComponent<InventorySystem>();
        playerData = GameManager.Instance.GetPlayerData();
        CloseWindow();
    }

    public void ExchangeItem()
    {
        int amount = inputChecker.GetValue();

        if (amount == -1)
        {
            Debug.LogError("잘못된 값이 입력됨");
            return;
        }

        if (inventory.CurrentFood() < amount)
        {
            Debug.Log("식량이 부족함.");
            return;
        }

        inventory.UseFood(amount);
        inventory.AddGear(amount);

    }

    public void OpenWindow()
    {
        gameObject.SetActive(true);
    }

    public void CloseWindow()
    {
        inputChecker.InitField();
        gameObject.SetActive(false);
    }
}
