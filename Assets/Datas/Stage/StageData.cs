using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 스테이지별로 고정된 데이터
/// </summary>
[CreateAssetMenu(menuName = "ScriptableObjects/StageData")]
public class StageData : ScriptableObject
{
    public string StageName => stageName;
    [SerializeField] string stageName;

    public SceneChanger.Scenes MapScene => mapScene;
    [SerializeField] SceneChanger.Scenes mapScene;

    public SceneChanger.Scenes LevelScene => levelScene; //
    [SerializeField] SceneChanger.Scenes levelScene;     // TODO: 레벨디자인 씬을 지정하는 자료형으로 변경

    public int RewardExp => rewardExp;
    [SerializeField, Tooltip("클리어 보상 경험치")] int rewardExp;

    public int RewardRation => rewardRation;
    [SerializeField, Tooltip("클리어 보상 식량")] int rewardRation;

    public GrenadeData Grenade => grenadeData;
    [SerializeField, Tooltip("해당 스테이지 수류탄 스펙")] GrenadeData grenadeData;
}
