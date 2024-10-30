using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MeleeData")]
public class MeleeWeaponData : ScriptableObject
{
    public AudioClip shotClip; // 타격? 소리
    

    public int damage = 100; // 공격력

    public int magCapacity = 0; // 탄창용량

    public float timeBetFire = 0.12f; // 탄알 발사 간격
    public float reloadTime = 1.8f; //재장전 소요 시간

    public float MeleeWeapon; // 근접무기 사거리
    public LayerMask layerMask; // 사격 가능한 대상(지형지물 고려할것)
}
