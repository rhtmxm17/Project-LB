using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
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
    [SerializeField] GunBase sampleKnife;

    [Header("고정 사용 아이템")]
    [SerializeField] GrenadeThrower grenadeThrow; // 3번 키, 수류탄 투척
    [SerializeField] CastingItem healthPack; // 4번 키, 회복 아이템

    [Space(5)]
    [SerializeField] Transform rightHandPose;
    [SerializeField] Transform weaponCamera;

    [Space(5)]
    [SerializeField] StatusDebuff hurtDebuffAsset;
    [SerializeField, Tooltip("그로기 기준 체력 비율")] float hurtReferenceValue = 0.4f;
    private float invHurtReference;
    private bool isHurt = false;

    [Header("Events")]
    public UnityEvent<int, int> OnMagazineUpdated; // 내용물 미구현
    public UnityEvent<int> OnSlotSeleted;
    public UnityEvent OnDead;

    public event UnityAction OnAttack;

    /// <summary>
    /// 자해 데미지 계수(수류탄)
    /// </summary>
    [SerializeField] float SelfDamageMultiplier = 0.1f;

    /// <summary>
    /// 장비 슬롯 개수
    /// </summary>
    public const int MaxSlot = 5;

    public const int MainWeaponSlot = 0;
    public const int MeleeWeaponSlot = 1;
    public const int GreanadeSlot = 2;
    public const int HealthPackSlot = 3;
    public const int SpecialWeaponSlot = 4;

    public int GrenadeUsage
    {
        get => grenadeThrow.Usage;
        set
        {
            grenadeThrow.Usage = value;
            NotifyMagazineUpdated();
        }
    }

    public int HealthPackUsage
    {
        get => healthPack.Usage;
        set
        {
            healthPack.Usage = value;
            NotifyMagazineUpdated();
        }
    }

    private readonly int[] WeaponSlots = new int[3] { MainWeaponSlot, MeleeWeaponSlot, SpecialWeaponSlot };

    private IUseable[] quickSlot = new IUseable[MaxSlot];
    private GunBase[] quickSlotGun = new GunBase[MaxSlot];

    private InputAction fireAction;
    private InputAction[] selectActions = new InputAction[MaxSlot];
    private InputAction reloadAction;
    private Dictionary<string, int> selectActionDict = new Dictionary<string, int>(10);
    private IUseable SelectedUseable => quickSlot[curSlotIndex];
    private int curSlotIndex;
    private Coroutine swapEquipRoutine;

    private PlayerModel model;

    public struct StageInitAttribute
    {
        public int maxHp;
        public GunBase mainWeapon;
        public GunBase meleeWeapon;
        public GunBase specialWeapon;
        public GrenadeData grenadeData;
    }

    /// <summary>
    /// 스테이지 시작시 초기화 함수
    /// </summary>
    /// <param name="attr">매개변수 세트</param>
    public void StageInit(StageInitAttribute attr)
    {
        model.MaxHp = attr.maxHp;
        model.Hp = model.MaxHp;

        if (sampleGun != null)
        {
            Destroy(sampleGun.gameObject);
            sampleGun = null;
        }

        if (sampleKnife != null)
        {
            Destroy(sampleKnife.gameObject);
            sampleKnife = null;
        }

        quickSlotGun[MainWeaponSlot] = attr.mainWeapon;
        quickSlotGun[MeleeWeaponSlot] = attr.meleeWeapon;
        quickSlotGun[SpecialWeaponSlot] = attr.specialWeapon;

        for (int i = 0; i < WeaponSlots.Length; i++)
        {
            int index = WeaponSlots[i];
            if (quickSlotGun[index] == null)
                continue;

            quickSlot[index] = quickSlotGun[index];

            quickSlotGun[index].WeaponCamera = weaponCamera;
            quickSlotGun[index].OnShot += InvokeAttack;
            quickSlotGun[index].OnShot += NotifyMagazineUpdated;
            quickSlotGun[index].OnReloadCompleted += NotifyMagazineUpdated;
            quickSlotGun[index].transform.SetParent(rightHandPose, false);
        }

        for (int i = 0; i < MaxSlot; i++)
        {
            if (quickSlot[i] == null)
                continue;

            quickSlot[i].gameObject.SetActive(false);
        }

        grenadeThrow.Data = attr.grenadeData;

        SelectedUseable.gameObject.SetActive(true);

        if (quickSlot[curSlotIndex] != null)
        {
            quickSlot[curSlotIndex].ShowAnimation(true);
        }

        NotifyMagazineUpdated();
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
        reloadAction = playerInput.actions["Reload"];

        for (int i = 0; i < quickSlot.Length; i++)
        {
            selectActions[i] = playerInput.actions[$"Select{i + 1}"];
            selectActionDict.Add(selectActions[i].name, i);
        }

        invHurtReference = 1f / hurtReferenceValue;

        quickSlot[GreanadeSlot] = grenadeThrow;
        grenadeThrow.OnThrow += NotifyMagazineUpdated;

        quickSlot[HealthPackSlot] = healthPack;
        healthPack.OnCasted += NotifyMagazineUpdated;
        healthPack.OnCasted += OnDrinkHelthPack;

        InitTesterWeapon();
    }

    private void InitTesterWeapon()
    {
        // 테스트용 무기 셋팅
        quickSlot[MainWeaponSlot] = sampleGun;
        quickSlot[MainWeaponSlot]?.ShowAnimation(true);
        quickSlotGun[MainWeaponSlot] = sampleGun;

        sampleGun.OnShot += InvokeAttack;
        sampleGun.OnShot += NotifyMagazineUpdated;
        sampleGun.OnReloadCompleted += NotifyMagazineUpdated;

        quickSlot[MeleeWeaponSlot] = sampleKnife;
        quickSlot[MeleeWeaponSlot]?.ShowAnimation(false);
        quickSlotGun[MeleeWeaponSlot] = sampleKnife;

        sampleKnife.OnShot += InvokeAttack;
        sampleKnife.OnShot += NotifyMagazineUpdated;
        sampleKnife.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        fireAction.started += FireStarted;
        fireAction.canceled += FireCanceled;
        reloadAction.started += Reload;

        for (int i = 0; i < quickSlot.Length; i++)
        {
            selectActions[i].started += OnSelectInput;
        }

        model.OnHpChange += HurtCheck;
    }

    private void OnDisable()
    {
        DisableInputAction();
    }

    private void DisableInputAction()
    {
        fireAction.started -= FireStarted;
        fireAction.canceled -= FireCanceled;
        reloadAction.started -= Reload;

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

    private void Reload(InputAction.CallbackContext _)
    {
        Debug.Log("리로드 키 입력");
        if (quickSlotGun[curSlotIndex] != null)
        {
            quickSlotGun[curSlotIndex].Reload();
        }
    }

    private void SelectSlot(int index)
    {
        // 현재 슬롯으로 변경 시도시 무시
        if (index == curSlotIndex)
            return;

        if (fireAction.inProgress)
        {
            Debug.Log("클릭 중인 상태로 교체 시도됨");
            return;
        }

        OnSlotSeleted?.Invoke(index);

        if (swapEquipRoutine != null)
        {
            StopCoroutine(swapEquipRoutine);
        }
        swapEquipRoutine = StartCoroutine(SwapEquipAnimation(curSlotIndex, index));

        curSlotIndex = index;
        NotifyMagazineUpdated();
    }

    private IEnumerator SwapEquipAnimation(int indexFrom, int indexTo)
    {
        quickSlot[indexFrom]?.ShowAnimation(false);

        if (quickSlot[indexTo] != null)
        {
            quickSlot[indexTo].gameObject.SetActive(true);
            quickSlot[indexTo]?.ShowAnimation(true);
        }

        yield return new WaitForSeconds(0.2f);

        quickSlot[indexFrom]?.gameObject.SetActive(false);
        swapEquipRoutine = null;
    }

    private void InvokeAttack() => OnAttack?.Invoke();

    private void NotifyMagazineUpdated()
    {
        if (curSlotIndex == GreanadeSlot)
        {
            OnMagazineUpdated.Invoke(GrenadeUsage, 0);
            return;
        }

        // TODO: 힐팩 개수
        if (curSlotIndex == HealthPackSlot)
        {
            OnMagazineUpdated.Invoke(HealthPackUsage, 0);
            return;
        }


        GunBase curGun = quickSlotGun[curSlotIndex];
        if (curGun != null)
        {
            OnMagazineUpdated.Invoke(curGun.MagazineRemain, curGun.BulletStock);
        }
        else
        {
            OnMagazineUpdated.Invoke(0, 0);
        }
    }

    public void Damaged(int damage, DamageType type = 0)
    {
        if (type == DamageType.FRENDLY_GROUP_0)
        {
            damage = (int)(damage * SelfDamageMultiplier);
        }

        model.Hp -= damage;

        if (model.Hp <= 0)
        {
            DisableInputAction();
            GetComponent<PlayerCharacterControllerControl>()?.DisableInput();

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

    private void OnDrinkHelthPack()
    {
        // 캐스팅 아이템 종류가 늘어난다면 프리펩 분리
        model.Hp += model.MaxHp >> 2;
    }
}
