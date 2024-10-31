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
    public string StageName => stageName;
    [SerializeField] string stageName;

    public SceneChanger.Scenes MapScene => mapScene;
    [SerializeField] SceneChanger.Scenes mapScene;

    /*public*/ int LevelScene => levelScene; //
    /*[SerializeField]*/ int levelScene;     // TODO: 레벨디자인 씬을 지정하는 자료형으로 변경

    public int RewardExp => rewardExp;
    [SerializeField, Tooltip("클리어 보상 경험치")] int rewardExp;

    public int RewardRation => rewardRation;
    [SerializeField, Tooltip("클리어 보상 식량")] int rewardRation;

    public GrenadeData Grenade => grenadeData;
    [SerializeField, Tooltip("해당 스테이지 수류탄 스펙")] GrenadeData grenadeData;
}
