using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/GunData")]
public class GunData : ScriptableObject
{
    public AudioClip shotClip; // 발사 소리
    public AudioClip reloadClip; // 재장전 소리

    public int damage = 50; // 공격력
    public int damageGrowth = 20; // 레벨별 피해량 증가

    public int magCapacity = 25; // 탄창용량

    public float timeBetFire = 0.12f; // 탄알 발사 간격
    public float reloadTime = 1.8f; //재장전 소요 시간

    public float range = 10; // 총기 사거리
    public LayerMask layerMask; // 사격 가능한 대상(지형지물 고려할것)
}
