using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어 캐릭터의 전투 기능<br/>
///  * 사용 아이템 선택 및 사용<br/>
///  * 피격시 처리
/// </summary>
[RequireComponent(typeof(PlayerModel))]
public class StagePlayerControl : MonoBehaviour, IDamageable
{
    [Header("테스트 셋팅 필드")]
    [SerializeField] GunBase sampleGun;

    [Space(5)]
    [SerializeField] StatusDebuff hurtDebuffAsset;
    [SerializeField, Tooltip("그로기 기준 체력 비율")] float hurtReferenceValue = 0.4f;
    private float invHurtReference;
    private bool isHurt = false;

    /// <summary>
    /// 자해 데미지 계수(수류탄)
    /// </summary>
    [SerializeField] float SelfDamageMultiplier = 0.1f;

    /// <summary>
    /// 장비 슬롯 개수
    /// </summary>
    public const int MaxSlot = 5;

    [SerializeField] IUseable[] quickSlot = new IUseable[MaxSlot];

    private InputAction fireAction;
    private InputAction[] selectActions = new InputAction[MaxSlot];
    private Dictionary<string, int> selectActionDict = new Dictionary<string, int>(10);
    private IUseable SelectedUseable => quickSlot[curSlotIndex];
    private int curSlotIndex;

    private PlayerModel model;

    private void Awake()
    {
        model = GetComponent<PlayerModel>();

        // InputAction 등록
        PlayerInput playerInput = PlayerInput.GetPlayerByIndex(0);
        if (playerInput == null)
        {
            Debug.LogWarning("씬에서 PlayerInput을 찾지 못함");
        }

        fireAction = playerInput.actions["Fire"];

        for (int i = 0; i < quickSlot.Length; i++)
        {
            selectActions[i] = playerInput.actions[$"Select{i + 1}"];
            selectActionDict.Add(selectActions[i].name, i);
        }

        invHurtReference = 1f / hurtReferenceValue;

        // 테스트 코드
        quickSlot[0] = sampleGun;
    }

    private void OnEnable()
    {
        fireAction.started += FireStarted;
        fireAction.canceled += FireCanceled;

        for (int i = 0; i < quickSlot.Length; i++)
        {
            selectActions[i].started += OnSelectInput;
        }

        model.OnHpChange += HurtCheck;
    }

    private void OnDisable()
    {
        fireAction.started -= FireStarted;
        fireAction.canceled -= FireCanceled;

        for (int i = 0; i < quickSlot.Length; i++)
        {
            selectActions[i].started -= OnSelectInput;
        }

        model.OnHpChange -= HurtCheck;
    }

    private void OnSelectInput(InputAction.CallbackContext context)
    {
        SelectSlot(selectActionDict[context.action.name]);
    }

    private void FireStarted(InputAction.CallbackContext _) => SelectedUseable?.UseBegin();

    private void FireCanceled(InputAction.CallbackContext _) => SelectedUseable?.UseEnd();

    private void SelectSlot(int index)
    {
        Debug.Log($"Selected {index}");

        if (fireAction.inProgress)
        {
            Debug.Log("클릭 중인 상태로 교체 시도될 경우의 처리 필요");
            return;
        }
        curSlotIndex = index;
    }

    public void Damaged(int damage, DamageType type = 0)
    {
        if (type == DamageType.FRENDLY_GROUP_0)
        {
            damage = (int)(damage * SelfDamageMultiplier);
        }

        model.Hp -= damage;
    }

    private void HurtCheck()
    {
        if (isHurt)
        {
            // 현재 체력이 기준 이상이라면
            if (model.Hp * invHurtReference > model.MaxHp)
            {
                isHurt = false;
                model.RemoveDebuff(hurtDebuffAsset);
            }
        }
        else
        {
            // 현재 체력이 기준 미만이라면
            if (model.Hp * invHurtReference < model.MaxHp)
            {
                isHurt = true;
                model.AddDebuff(hurtDebuffAsset);
            }
        }
    }
}
