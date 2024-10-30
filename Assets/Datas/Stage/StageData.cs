using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지별로 고정된 데이터
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/StageData")]
public class StageData : ScriptableObject
{
    /*
    
    1. 지형 로드 <-여기선 플레이어 참조 필요x
    2. 플레이어 + 트리거 로드
     
     */
    [SerializeField] string stageName;
    [SerializeField] SceneChanger.Scenes mapScene;
    [SerializeField] int i; // TODO: 트리거 데이터 씬
    [SerializeField, Tooltip("클리어 보상 경험치")] int rewardExp;
    [SerializeField, Tooltip("클리어 보상 식량")] int rewardRation;

    [SerializeField, Tooltip("해당 스테이지 수류탄 스펙")] GrenadeData grenadeDatas;
}
