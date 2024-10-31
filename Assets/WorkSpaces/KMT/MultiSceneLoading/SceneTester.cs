using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTester : MonoBehaviour
{
    
    SceneChanger changer;

    private void Awake()
    {
        changer = GameManager.Instance.GetSceneChanger();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        { 
            changer.ChangeToMultiScene(SceneChanger.Scenes.STAGE1, SceneChanger.Scenes.STAGE2);
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            changer.ChangeScene(SceneChanger.Scenes.BUNCKER);
        }
    }

}
