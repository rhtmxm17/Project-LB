using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChanger : MonoBehaviour
{
    public enum Scenes { BUNCKER, STAGE1, STAGE2, STAGE3, STAGE4 }

    [Header("Panel")]
    [SerializeField]
    GameObject loadingPanel;

    [Header("Fader")]
    [SerializeField]
    Animator fader;

    public UnityEvent OnLoadSceneComplete;

    string destStr;
    List<Scenes> destSceneTypes = new List<Scenes>();

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

        //애니메이션 시작. ( 페이드 아웃 , 인 )
        fader.Play("LoadingIn");

    }

    public void ChangeToMultiScene(params Scenes[] destScene)
    {

        foreach (Scenes sceneType in destScene)
        {
            if (GetSceneName(sceneType) == null)
            {
                Debug.LogError("전환할 씬이 존재하지 않음");
                return;
            }
        }

        destSceneTypes = destScene.ToList();

        //애니메이션 시작. ( 페이드 아웃 , 인 )
        fader.Play("MultiLoadingIn");
    }

    public void StartLoading() 
    {
        StartCoroutine(Load(destStr));
    }

    public void StartMuiltiLoading(UnityAction onLoadSceneCompleteCallback = null)
    {
        OnLoadSceneComplete.RemoveAllListeners();
        if (onLoadSceneCompleteCallback != null)
            OnLoadSceneComplete.AddListener(onLoadSceneCompleteCallback);

        StartCoroutine(MultiLoad(destSceneTypes));
    }

    //후처리
    void EndLoadScene() { 
    

    }

    IEnumerator MultiLoad(List<Scenes> destSceneTypes)
    {

        //첫 씬은 넘어가야 함.
        AsyncOperation baseStatus = SceneManager.LoadSceneAsync(GetSceneName(destSceneTypes[0]));
        destSceneTypes.RemoveAt(0);

        baseStatus.allowSceneActivation = false;

        while (baseStatus.isDone == false)
        {

            if (baseStatus.progress >= 0.9f)
            {
                //로딩완료
                //씬 전환
                baseStatus.allowSceneActivation = true;

            }

            yield return null;

        }

        //유예시간
        yield return new WaitForSeconds(2f);

        //로딩할 씬이 남아있음
        if (destSceneTypes.Count > 0)
        {

            List<AsyncOperation> lodingOperL = new List<AsyncOperation>();
            bool isLoading = true;

            //병렬 로딩 시작
            foreach (Scenes sceneType in destSceneTypes)
            {
                AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(GetSceneName(sceneType), LoadSceneMode.Additive);
                //일부 씬이 로딩 완료되어도 개별적으로 전환하지 않도록 설정.
                asyncLoad.allowSceneActivation = false;
                lodingOperL.Add(asyncLoad);
            }

            //매 프래임마다 모든 씬이 로딩되었는지 확인.
            while (isLoading)
            {
                isLoading = true;

                foreach (AsyncOperation state in lodingOperL)
                {
                    //아직 로딩중인 씬이 있을 경우
                    if (!state.isDone && state.progress < 0.9f)
                    {
                        isLoading = false;
                        break;
                    }
                }

                yield return null;
            }

            //로딩 완료이므로 모든 씬을 활성화.
            foreach (AsyncOperation state in lodingOperL)
            {
                state.allowSceneActivation = true;
            }

        }

        //로딩 완료시 접근할 수 있는 코드 영역
        OnLoadSceneComplete?.Invoke();

        //todo : 로딩바 확인용 더미. 나중에 제거 (로딩완료씬 더미딜레이, 제거됨)                 
        yield return new WaitForSeconds(3f);

        fader.Play("LoadingOut");

    }


    IEnumerator Load(string sceneName)
    {

        AsyncOperation op = SceneManager.LoadSceneAsync(sceneName);

        op.allowSceneActivation = false;

        while (op.isDone == false)
        {

            if (op.progress < 0.9f)
            {

                //로딩중

            }
            else
            {

                //todo : 로딩바 확인용 더미. 나중에 제거 (로딩완료씬 더미딜레이, 제거됨)                 
                yield return new WaitForSeconds(3f);                

                //로딩 완료
                //씬 전환
                op.allowSceneActivation = true;
                fader.Play("LoadingOut");

            }

            yield return null;

        }

    }

}
