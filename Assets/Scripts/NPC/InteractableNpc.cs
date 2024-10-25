using UnityEngine;
using UnityEngine.Events;

public class InteractableNpc : NpcBase, IInteractable
{

    [SerializeField]
    protected UnityEvent OnInteractedEvent;

    [Header("NormalDialogue")]
    [SerializeField]
    protected DialogueSO normalDialogue;

    [Header("Is Randomized Text")]
    [SerializeField]
    protected bool isRadomize;

    protected int textIdx = 0;

    public virtual void OnInteracted()
    {

        //출력할 대사.
        string text = GetNormalText();

        //todo : 창 띄우기. => 대사를 매개변수로 전달하기? || 내가 ui객체를 가지고있기?
        OnInteractedEvent?.Invoke();

    }


    protected string GetNormalText() {

        if (isRadomize)
        {
            textIdx = Random.Range(0, normalDialogue.Texts.Length);
        }
        else
        {
            textIdx++;
            textIdx %= normalDialogue.Texts.Length;
        }

        return normalDialogue.Texts[textIdx];

    }

}
