using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLoadWindow : MonoBehaviour
{
    [SerializeField]
    StageSelectWindow stageSelectWindow;

    public void OverriteButton()
    {
        FileIOSystem.SavePlayerData(GameManager.Instance.GetPlayerData());
        gameObject.SetActive(false);
    }

    public void LoadDataButton()
    {
        //todo : 본인씬이므로 나중에 테스트되면 벙ㅇ커씬으로 수정하기.
        GameManager.Instance.ReLoadData();
        GameManager.Instance.GetSceneChanger().ChangeScene(SceneChanger.Scenes.BUNCKER);
        stageSelectWindow.CloseWindow();
    }
}
