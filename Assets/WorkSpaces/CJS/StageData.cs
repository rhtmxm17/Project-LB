using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지별로 고정된 데이터
/// </summary>
public class StageData : ScriptableObject
{

    [SerializeField] string stageName;
    [SerializeField, Tooltip("클리어 보상 경험치")] int rewardExp;
    [SerializeField, Tooltip("클리어 보상 식량")] int rewardRation;

    [SerializeField, Tooltip("해당 스테이지 수류탄 스펙")] List<GrenadeData> grenadeDatas;

}
