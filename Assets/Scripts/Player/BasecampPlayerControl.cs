using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 플레이어 캐릭터 조작 중 베이스캠프에서 사용하는 내용을 담당한다.
/// </summary>
public class BasecampPlayerControl : MonoBehaviour
{
    [SerializeField] Transform interactPose;
    [SerializeField] float interactRadius = 3f;

    private InputAction interactAction;
    private Collider[] overlapResults = new Collider[4];
    private int interactableLayer;

    private void Awake()
    {
        if (interactPose == null)
        {
            interactPose = new GameObject("Interact Pose").transform;
            interactPose.parent = this.transform;
            interactPose.localPosition = 0.5f * interactRadius * Vector3.forward;
        }

        PlayerInput playerInput = PlayerInput.GetPlayerByIndex(0);
        if (playerInput == null)
        {
            Debug.LogWarning("씬에서 PlayerInput을 찾지 못함");
        }

        interactAction = playerInput.actions["Interact"];

        interactableLayer = LayerMask.GetMask("Interactable");
    }

    private void OnEnable()
    {
        interactAction.started += TryInteract;
    }

    private void OnDisable()
    {
        interactAction.started -= TryInteract;
    }

    private void TryInteract(InputAction.CallbackContext _)
    {
        int count = Physics.OverlapSphereNonAlloc(interactPose.position, interactRadius, overlapResults, interactableLayer);
        if (count == 0)
            return;

        // 감지된 대상중 가장 가까운 대상 검사
        float minSqrDistance = float.MaxValue;
        int minDistanceIndex = 0;

        for (int i = 0; i < count; i++)
        {
            float sqrDistance = (this.transform.position - overlapResults[i].transform.position).sqrMagnitude;
            if (minSqrDistance > sqrDistance)
            {
                minSqrDistance = sqrDistance;
                minDistanceIndex = i;
            }
        }

        GameObject target = overlapResults[minDistanceIndex].attachedRigidbody?.gameObject ?? overlapResults[minDistanceIndex].gameObject;
        Debug.Log($"{target.name}에 대한 상호작용 시도");

        if(target.TryGetComponent(out IInteractable interactable))
        {
            interactable.OnInteracted();
        }
    }
}
