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

    public GunBase GunPrefab => gunPrefab;
    [SerializeField] GunBase gunPrefab;

    public AudioClip ShotClip => shotClip; // 발사 소리
    [SerializeField] AudioClip shotClip;

    public AudioClip ReloadClip => reloadClip; // 재장전 소리
    [SerializeField] AudioClip reloadClip;

    public int damage = 50; // 공격력
    public int damageGrowth = 20; // 레벨별 피해량 증가

    public int magCapacity = 25; // 탄창용량

    public float timeBetFire = 0.12f; // 탄알 발사 간격
    public float reloadTime = 1.8f; //재장전 소요 시간

    public float range = 10; // 총기 사거리
    public LayerMask layerMask; // 사격 가능한 대상(지형지물 고려할것)

}
