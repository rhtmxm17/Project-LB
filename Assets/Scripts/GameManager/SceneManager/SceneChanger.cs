using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public enum Scenes { BUNCKER, STAGE1, STAGE2, STAGE3, STAGE4 }

    [Header("Panel")]
    [SerializeField]
    GameObject loadingPanel;
    [SerializeField]
    Slider slider;

    [Header("Fader")]
    [SerializeField]
    Animator fader;

    string destStr;

    private void Awake()
    {
        loadingPanel.gameObject.SetActive(false);
    }

    //디버그용

    [System.Serializable]
    public struct sceneBlock {

        [SerializeField]
        public Scenes sceneType;
        [SerializeField]
        public string sceneName;

    }

    [SerializeField]
    sceneBlock[] sceneInfoArr;

    string GetSceneName(Scenes sceneType) {

        foreach (var scene in sceneInfoArr) {

            if (scene.sceneType == sceneType) {

                return scene.sceneName;
            }

        }

        return null;

    }



    public void ChangeScene(Scenes destScene) {

        destStr = GetSceneName(destScene);

        if (destStr == null) {

            Debug.LogError("전환할 씬이 존재하지 않음");
            return;

        }

        slider.value = 0f;
        //애니메이션 시작. ( 페이드 아웃 , 인 )
        fader.Play("LoadingIn");

    }

    public void StartLoading() 
    {
        StartCoroutine(Load(destStr));
    }

    //후처리
    void EndLoadScene() { 
    

    }

    IEnumerator Load(string _sceneName)
    {

        AsyncOperation op = SceneManager.LoadSceneAsync(_sceneName);

        op.allowSceneActivation = false;

        while (op.isDone == false)
        {

            if (op.progress < 0.9f)
            {

                //로딩중

            }
            else
            {

                //todo : 로딩바 확인용 더미. 나중에 제거
                yield return new WaitForSeconds(0.5f);
                slider.value = 0.2f;
                yield return new WaitForSeconds(0.5f);
                slider.value = 0.5f;
                yield return new WaitForSeconds(0.5f);
                slider.value = 0.75f;
                yield return new WaitForSeconds(0.5f);
                slider.value = 1f;

                //로딩 완료
                //씬 전환
                op.allowSceneActivation = true;
                fader.Play("LoadingOut");

            }

            yield return null;

        }

    }

}
