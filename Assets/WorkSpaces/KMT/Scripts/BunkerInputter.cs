using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerInputter : MonoBehaviour
{
    [SerializeField]
    PlayerCharacterControllerControl control;

    [SerializeField]
    InventorySystem inventorySystem;
    [SerializeField]
    PlayerInfoSystem playerInfoSystem;
    [SerializeField]
    UpgradeSystem upgradeSystem;
    [SerializeField]
    ExchangeSystem exchangeSystem;
    [SerializeField]
    StageSelectWindow stageSelectWindow;

    public void CloseAllWindow()
    {
        control.MouseLock(true);

        inventorySystem.CloseWindow();
        playerInfoSystem.CloseWindow();
        upgradeSystem.CloseWindow();
        exchangeSystem.CloseWindow();
        stageSelectWindow.CloseWindow();
    }

    public void Quit()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    private void Start()
    {
        GameManager.Instance.GetSoundManager().StopBGM();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventorySystem.OpenWindow();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            playerInfoSystem.OpenWindow();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAllWindow();
        }

    }
}
