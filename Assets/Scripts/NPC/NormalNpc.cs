using TMPro;
using UnityEngine;

public class NormalNpc : NpcBase
{

    [SerializeField]
    DialogueSO dialogueArr;

    [SerializeField]
    protected TextMeshProUGUI tmp;
    protected Animator animator;

    protected virtual void Awake()
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

    protected virtual void ShowDialogue()
    {

        int idx = Random.Range(0, dialogueArr.Texts.Length);
        tmp.text = dialogueArr.Texts[idx];
        animator.Play("FadeIn");

    }

    protected virtual void HideDialogue()
    {
        animator.Play("FadeOut");
    }
}
