using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(menuName = "Inventory/Items/EquableItem")]
public class InventoryEquableItemSO : InventoryInfoItemSO
{
    //todo : 인터페이스 제거
    [field: Header("Equip Type")]
    [field: SerializeField]
    public EquipSlot equipQuickSlotType {  get; private set; }

    public bool IsEquip {  get; private set; }

    [field: Header("Param")]
    [field: SerializeField]
    public int AttackPoint { get; private set; }
    [field: SerializeField]
    public int AdditiveAttackPoint { get; private set; }

    [field: SerializeField]
    public int MaxUpgradeLevel { get; private set; }
    
    [field: SerializeField]
    public int[] UpgradeReqGears { get; private set; }

    public GunBase GunPrefab => gunPrefab;
    [SerializeField] GunBase gunPrefab;

    public AudioClip ShotClip => shotClip; // 발사 소리
    [SerializeField] AudioClip shotClip;

    public AudioClip ReloadClip => reloadClip; // 재장전 소리
    [SerializeField] AudioClip reloadClip;

    public int MagCapacity => magCapacity; // 탄창용량
    [SerializeField, Tooltip("재장전 한번에 채워지는 최대 탄창수, 음수라면 탄창과 무관한 무기(예: 단검)")] int magCapacity = 25;

    public int BulletStock => bulletStock;
    [SerializeField, Tooltip("재장전에 사용될 탄약, 음수라면 재장전 제한 없음")] int bulletStock = -1;

    public float TimeBetFire => timeBetFire; // 탄알 발사 간격
    [SerializeField] float timeBetFire = 0.12f;

    public float ReloadTime = 1.8f; //재장전 소요 시간

    public float Range = 10; // 총기 사거리
    public LayerMask layerMask; // 사격 가능한 대상(지형지물 고려할것)

}
