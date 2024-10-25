using TMPro;
using UnityEngine;

public class NormalNpc : NpcBase
{

    [SerializeField]
    DialogueSO dialogueArr;

    [SerializeField]
    TextMeshProUGUI tmp;
    Animator animator;

    private void Awake()
    {
        animator = tmp.transform.gameObject.GetComponent<Animator>();
    }

    protected override void OnTriggerEnterCallback()
    {
        ShowDialogue();
    }

    protected override void OnTriggerExitCallback()
    {
        HideDialogue();
    }

    void ShowDialogue()
    {

        int idx = Random.Range(0, dialogueArr.Texts.Length);
        tmp.text = dialogueArr.Texts[idx];
        animator.Play("FadeIn");

    }

    void HideDialogue()
    {
        animator.Play("FadeOut");
    }
}
