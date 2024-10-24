using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] float moveSpeed;

    private NavMeshAgent agent;
    private InputAction moveAction;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        moveAction = playerInput.actions["Move"];

        StartCoroutine(MovementRoutine());
    }

    private IEnumerator MovementRoutine()
    {
        while (true)
        {
            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            //
            // 카메라의 up, right를 xz평면에서의 이동 방향으로 변환
            Vector3 moveAxisX = Camera.main.transform.right;
            Vector3 moveAxisY = Camera.main.transform.forward;

            moveAxisX.y = 0f;
            moveAxisY.y = 0f;

            moveAxisX.Normalize();
            moveAxisY.Normalize();

            // 입력 방향과 속도 능력치를 적용
            Vector3 velocity = moveSpeed * (moveAxisX * moveInput.x + moveAxisY * moveInput.y);
            agent.Move(Time.deltaTime * velocity);

            yield return null;
        }
    }
}
