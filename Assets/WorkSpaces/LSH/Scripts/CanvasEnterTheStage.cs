using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasEnterTheStage : MonoBehaviour
{
    [SerializeField] GameObject stageChoiceUI; /// 씬 진짜 이동할꺼냐고 묻는 UI
    [SerializeField] TMP_Text stageChoiceText; /// 이동할 씬 이름을 출력해주는 TMP
    int stageNumber;                           /// StageEntered함수의 파라미터로 넘어온 Num을 저장하는 int
    [SerializeField] Button btnYes;     ///씬 진짜 이동하겠다는 버튼
    [SerializeField] Button btnNo;     /// 씬 이동 안하겠다는 버튼


    private void Start()
    {
        stageNumber = -1;
    }


    /// <summary>
    /// 누른 버튼에 해당하는 씬 이름을 출력해주는 함수입니다. stageChoiceUI를 SetActive합니다.
    /// </summary>
    public void StageEntered(int Num)
    {
        stageNumber = Num;

        switch (Num)
        {
            case 1:
                stageChoiceText.text = "ARE YOU SURE YOU WANT TO PLAY [stage1 고요한 숲]?";
                break;

            case 2:
                stageChoiceText.text = "ARE YOU SURE YOU WANT TO PLAY [Stage2 황폐한 공장 지대]?";
                break;

            case 3:
                stageChoiceText.text = "ARE YOU SURE YOU WANT TO PLAY [Stage3 잃어버린 벙커]?";
                break;

            case 4:
                stageChoiceText.text = "ARE YOU SURE YOU WANT TO PLAY [Boss 잃어버린 벙커의 비밀방]?";
                break;

        }

        stageChoiceUI.SetActive(true);

    }


    /// <summary>
    /// 선택받은 씬으로 정말 이동할 수 있는 함수입니다. 씬이 로드됩니다.
    /// </summary>
    public void YesGoToStage()
    {
        //FIXME: 씬 로드 연결 부탁드립니다
        //SceneManager.LoadScene(stageNumber);
        Debug.LogWarning($"FIXME: {stageNumber}번 씬 로드하는 기능 연결 부탁드립니다");
    }

    /// <summary>
    /// 선택받은 씬으로 이동하지 않고 해당 창을 꺼버리는 함수입니다.
    /// </summary>
    public void NoStayhere()
    {
        stageChoiceUI.SetActive(false);
    }

}
