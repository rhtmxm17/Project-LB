using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어 캐릭터의 전투 기능<br/>
///  * 사용 아이템 선택 및 사용<br/>
///  * 피격시 처리
/// </summary>
[RequireComponent(typeof(PlayerModel))]
public class StagePlayerControl : MonoBehaviour, IDamageable
{
    [Header("테스트 필드")]
    [SerializeField] GunBase sampleGun;

    [Header("고정 사용 아이템")]
    [SerializeField] GrenadeThrower grenadeThrow; // 3번 키, 수류탄 투척
    [Space(5)]

    [SerializeField] StatusDebuff hurtDebuffAsset;
    [SerializeField, Tooltip("그로기 기준 체력 비율")] float hurtReferenceValue = 0.4f;
    private float invHurtReference;
    private bool isHurt = false;

    [Header("Events")]
    public UnityEvent<int> OnMagazineUpdated; // 내용물 미구현
    public UnityEvent<int> OnSlotSeleted;
    public UnityEvent OnDead;

    /// <summary>
    /// 자해 데미지 계수(수류탄)
    /// </summary>
    [SerializeField] float SelfDamageMultiplier = 0.1f;

    /// <summary>
    /// 장비 슬롯 개수
    /// </summary>
    public const int MaxSlot = 5;

    [SerializeField] IUseable[] quickSlot = new IUseable[MaxSlot];

    public event UnityAction OnAttack;
    private InputAction fireAction;
    private InputAction[] selectActions = new InputAction[MaxSlot];
    private Dictionary<string, int> selectActionDict = new Dictionary<string, int>(10);
    private IUseable SelectedUseable => quickSlot[curSlotIndex];
    private int curSlotIndex;

    private PlayerModel model;


    public struct StageInitAttribute
    {
        public int maxHp;
        public int mainWeaponLevel;
        public InventoryEquableItemSO mainWeapon;
        public InventoryEquableItemSO meleeWeapon;
        public GrenadeData grenadeData;

        public InventoryEquableItemSO specialWeapon;
    }

    /// <summary>
    /// 스테이지 시작시 초기화 함수
    /// </summary>
    /// <param name="attr">매개변수 세트</param>
    public void StageInit(StageInitAttribute attr)
    {
        sampleGun.DataTable = attr.mainWeapon;
        sampleGun.GunLevel = attr.mainWeaponLevel;
        grenadeThrow.Data = attr.grenadeData;
    }

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
        sampleGun.OnShot += InvokeAttack;
        quickSlot[2] = grenadeThrow;
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

    private void FireStarted(InputAction.CallbackContext _)
    {
        SelectedUseable?.UseBegin();
    } 

    private void FireCanceled(InputAction.CallbackContext _)
    {
        SelectedUseable?.UseEnd();
    }

    private void SelectSlot(int index)
    {
        OnSlotSeleted?.Invoke(index);

        if (fireAction.inProgress)
        {
            Debug.Log("클릭 중인 상태로 교체 시도될 경우의 처리 필요");
            return;
        }
        curSlotIndex = index;
    }

    private void InvokeAttack() => OnAttack?.Invoke();

    public void Damaged(int damage, DamageType type = 0)
    {
        if (type == DamageType.FRENDLY_GROUP_0)
        {
            damage = (int)(damage * SelfDamageMultiplier);
        }

        model.Hp -= damage;

        if (model.Hp < 0)
        {
            OnDead?.Invoke();
        }
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
