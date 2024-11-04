using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// UI 프리팹 내부의 필요한 참조를 전달하는 클래스
/// </summary>
public class GameOverUI : MonoBehaviour
{
    [SerializeField] Button retryButton;
    [SerializeField] Button returnButton;

    /// <summary>
    /// 재도전 버튼 클릭시 이벤트
    /// </summary>
    public UnityEvent OnRetryButtonClicked => retryButton.onClick;

    /// <summary>
    /// 벙커로 돌아가기 버튼 클릭시 이벤트
    /// </summary>
    public UnityEvent OnReturnButtonClicked => returnButton.onClick;

    private void Start()
    {
        GameManager.Instance.GetStageSceneManager().InitGameOverUI(this);
    }
}
