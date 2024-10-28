using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerModel))]
public class StagePlayerControl : MonoBehaviour, IDamageable
{
    public const int MaxSlot = 5;

    [SerializeField] IUseable[] quickSlot = new IUseable[MaxSlot];

    private InputAction fireAction;
    private IUseable SelectedUseable => quickSlot[curSlotIndex];
    private int curSlotIndex;

    private PlayerModel model;

    private void Awake()
    {
        model = GetComponent<PlayerModel>();

        PlayerInput playerInput = PlayerInput.GetPlayerByIndex(0);
        if (playerInput == null)
        {
            Debug.LogWarning("씬에서 PlayerInput을 찾지 못함");
        }
        fireAction = playerInput.actions["Fire"];   
    }

    private void OnEnable()
    {
    }

    private void SelectSlot(int index)
    {
        curSlotIndex = index;
    }

    public void Damaged(int damage, DamageType type = 0)
    {
        model.Hp -= damage;
    }
}
