using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class StagePlayerControl : MonoBehaviour
{
    public const int MaxSlot = 5;

    [SerializeField] PlayerInput playerInput;
    [SerializeField] IUseable[] quickSlot = new IUseable[MaxSlot];

    private InputAction fireAction;
    private IUseable SelectedUseable => quickSlot[curSlotIndex];
    private int curSlotIndex;

    private void Awake()
    {
        fireAction = playerInput.actions["Fire"];   
    }

    private void OnEnable()
    {
    }

    private void SelectSlot(int index)
    {
        curSlotIndex = index;
    }
}
