using UnityEngine;
using UnityEngine.Events;

public class InteractableNpc : NormalNpc　, IInteractable
{

    [SerializeField]
    protected UnityEvent OnInteractedEvent;

    bool isInteracted = false;


    [SerializeField] GameObject InteractionButton;
    public UnityEvent OnCommunication;



    private void Start()
    {
        InteractionButton.SetActive(false);

    }


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

        //LSH: 플레이어 인식때 실행될 코드 (E 버튼 이미지 Off)
        InteractionButton.SetActive(false);


    }

    protected override void OnTriggerEnterCallback()
    {
        base.OnTriggerEnterCallback();

        //LSH: 플레이어 인식때 실행될 코드 (E 버튼 이미지 On, E 버튼 눌러서 상호작용 시도)
        InteractionButton.SetActive(true);

        Debug.Log(1);

        // FIXME : 유아이 열어달라는 이벤트 호출 (BasecampPlayerControl로)
        OnCommunication?.Invoke();

    }




}
