using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 게임플레이중에 발생할 각 상황에 대한 UI 처리 함수 public으로 만들기위한 스크립트
public class InGameUiManager : MonoBehaviour
{
    
    // 스크립트 가져오기
    [SerializeField] PlayerModel playerHP;

    // 유아이 캔버스
    [SerializeField] GameObject gameOverUI; // 게임오버 UI
    [SerializeField] GameObject gameClearUI; // 겜클리어 UI

    // 버튼, 이미지, 텍스트들
    [SerializeField] Image[] SlotImage; // 퀵슬롯 이미지들
    [SerializeField] Image[] ChoiceSlot; //선택중인 퀵슬롯에 표시될 강조이미지emf
    [SerializeField] Image curGunImg;
    [SerializeField] TMP_Text remainBullet;


    private void Start()
    {
        gameOverUI.SetActive(false);
        gameClearUI.SetActive(false);

        //총알 갯수가 바뀔때마다 호출될 함수
        //퀵슬롯 선택변경시 호출될 이벤트함수
        //플레이어 사망시 넘어올 이벤트함수
        //스테이지 클리어시 넘어올 이벤트함수

        //강조 이미지를 퀵슬롯 1번칸 위치로 초기화
        SetChoiceSlot(0);
    }


    ///----------------------------------------------------
    ///----------------------------------------------------
    ///----------------------------------------------------

    

    /// <summary>
    /// 총알 갯수가 바뀔때마다 호출될 함수입니다. 남은 총알 수를 파라미터로 받습니다.
    /// </summary>
    /// /// <param name="remainedBullet"> 잔탄 수를 의미합니다. </param>  
    public void ChangeNumOfBullet(int remainedBullet)
    {
        remainBullet.text = $"{remainedBullet} / 999"; 
        // remainedBullet은 한 탄창에서 1발씩 없어지는 모습이 출력되고, 999는 보유한 전체 총알수를 출력할 의도입니다.

    }



    /// <summary>
    /// 퀵슬롯의 n번째 칸을 선택했을때 호출될 함수입니다. 선택된 번호 n번째 칸의 슬롯을 강조 표시합니다.
    /// </summary>
    /// /// <param name="num">지정할 퀵슬롯의 번호를 의미합니다. (범위: 0~4)</param>
    public void SetChoiceSlot(int num)
    {
        /// 퀵슬롯에 등록된 내용이 변경될 때가 아닌,
        /// 퀵슬롯 1번 선택중인데 n번으로 선택할래 <--의 경우 호출되는 함수

        //벙커에서 인게임 들어오기 전에 선택한 총기 정보를 받아옴
        /// 스테이지 들어갈때, 1 2 5번의 무기를 미리 선택한 후, 이미 정하고 들어가기 때문에
        /// 1 2 5번에 들어갈 이미지를 스테이지 매니저한테 넣어달라고 요청한 후에 
        /// 설정된 채로 인게임 진입하는게 맞는 순서라고 판단했습니다.

        //강조표시
        ///강조 이미지를 QuickSlotButtons[QuickNum] QuickNum번칸 위치로 옮김 (배열의 QuickNum-1번)
        for (int i = 0; i < 5; i++)
        {
            ChoiceSlot[i].enabled = false;
        }
        ChoiceSlot[num].enabled = true;

        //선택한 총기의 이미지를 오른쪽 하단에도 띄움 (CurGun)
        /// 인게임에 진입 후, 1 2 5번 슬롯이 선택됐다면
        /// 해당 총기 이미지를 CurGun에 띄우는 기능도 추가.
        curGunImg.sprite = SlotImage[num].sprite;
        

    }



    /// <summary>
    /// 플레이어 사망시 호출될 함수입니다. 사망 UI를 Active해줍니다.
    /// </summary>
    public void PlayerDead()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;

    }


    
    /// <summary>
    /// 스테이지를 무사히 클리어하면 호출되는 함수입니다. 클리어 UI를 Active해줍니다.
    /// </summary>
    public void StageClear()
    {
        gameClearUI.SetActive(true);
        Time.timeScale = 0f;

    }






}
