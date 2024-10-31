using UnityEngine;
using UnityEngine.Events;

public class InteractableNpc : NormalNpc　, IInteractable
{

    [SerializeField]
    protected UnityEvent OnInteractedEvent;

    bool isInteracted = false;

    public virtual void OnInteracted()
    {
        isInteracted = true;
        animator.Play("Hide");
        //todo : 창 띄우기. => 대사를 매개변수로 전달하기? || 내가 ui객체를 가지고있기?
        OnInteractedEvent?.Invoke();

    }

    protected override void OnTriggerExitCallback()
    {

        if (isInteracted)
        {
            isInteracted = false;
        }
        else
        {
            base.OnTriggerExitCallback();
        }


    }


}
