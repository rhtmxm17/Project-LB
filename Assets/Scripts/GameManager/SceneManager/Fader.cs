using UnityEngine;

public class Fader : MonoBehaviour
{
    [SerializeField]
    GameObject loadPanel;
    [SerializeField]
    SceneChanger changer;

    public void ShowWindow()
    {
        loadPanel.SetActive(true);
    }
    public void HideWindow()
    {
        loadPanel.SetActive(false);
    }

    public void StartLoading()
    {
        changer.StartLoading();
    }

    public void StartMultiLoading()
    {
        changer.StartMuiltiLoading();
    }
}
