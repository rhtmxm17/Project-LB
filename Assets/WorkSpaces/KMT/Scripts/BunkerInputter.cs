using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunkerInputter : MonoBehaviour
{
    [SerializeField]
    InventorySystem inventorySystem;
    [SerializeField]
    PlayerInfoSystem playerInfoSystem;

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
    }
}
