using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class Retry : MonoBehaviour
{
    StageSceneManager stageSceneManager;
    Button button;

    // Start is called before the first frame update
    void Start()
    {
        stageSceneManager = GameManager.Instance.GetStageSceneManager();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClicked);
    }

    void OnClicked()
    {
        stageSceneManager.EnterStage();
    }

    
}
