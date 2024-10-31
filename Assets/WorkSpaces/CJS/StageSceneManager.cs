using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSceneManager : MonoBehaviour
{
    /*
     스테이지 매니저에서 처리할 목록

    생성시 필요한 데이터
        스테이지별 고정된 데이터 (StageData : ScriptableObject)
            지형 데이터
            몬스터 배치 데이터
            보상
            수류탄 스펙
        플레이어
            체력 혹은 레벨
        선택된 무기 목록
            기본 무기 레벨
        
    UI 띄우기 -> 해당 UI 작업 담당자와 논의 필요
        클리어 이벤트 구독해서 UI 띄우기
        플레이어 사망 이벤트 구독해서 게임오버 UI 띄우기
    스테이지 종료시 데이터 관리자에 결과 통지(획득한 재화 및 경험치, 수집품)

     */

    /// <summary>
    /// 해당 스테이지 씬으로 진입
    /// </summary>
    /// <param name="data"></param>
    public void EnterStage(StageData data)
    {
        // TODO:
        //  지형 데이터, 유닛 데이터 불러오기
        //  씬 로드 완료 이벤트에 스테이지 초기화 함수 구독 붙이기

    }

    private void InitStage()
    {
        PlayerModel player = GameManager.Instance.GetPlayerModel();
        if (player == null)
        {
            Debug.LogError("플레이어 캐릭터를 찾을 수 없음");
            return;
        }

        if (player.TryGetComponent(out StagePlayerControl playerControl))
        {
            Debug.LogError("스테이지용 플레이어 캐릭터가 아님");
            return;
        }

        // 플레이어 초기화
        //playerControl.StageInit()
    }
}