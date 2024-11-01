using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerModel), typeof(Rigidbody))]
public class PlayerRigidBodyControl : MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] float maxVerticalCameraAngle = 70f;
    [SerializeField] Vector2 cameraSensitivity = Vector2.one;
    [SerializeField] float moveAccel = 15f; // 가속도

    private InputAction moveAction;
    private InputAction lookAction;
    private PlayerModel model;
    private Rigidbody rigidbody;
    private float MoveSpeed => model.MoveSpeed; // 최대속도
    private float verticalCameraAngle;
    private Coroutine moveRoutine;

    private void Awake()
    {
        model = GetComponent<PlayerModel>();
        rigidbody = GetComponent<Rigidbody>();

        PlayerInput playerInput = PlayerInput.GetPlayerByIndex(0);
        if (playerInput == null)
        {
            Debug.LogWarning("씬에서 PlayerInput을 찾지 못함");
        }
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
    }

    private void OnEnable()
    {
        moveAction.started += MoveActionStarted;
        moveAction.canceled += MoveActionCanceled;
        lookAction.performed += RotatePlayerLook;
    }

    private void OnDisable()
    {
        moveAction.started -= MoveActionStarted;
        moveAction.canceled -= MoveActionCanceled;
        lookAction.performed -= RotatePlayerLook;
    }

    private void MoveActionStarted(InputAction.CallbackContext _) => moveRoutine = StartCoroutine(MovementRoutine());

    private void MoveActionCanceled(InputAction.CallbackContext _) => StopCoroutine(moveRoutine);

    private IEnumerator MovementRoutine()
    {
        YieldInstruction waitFixedUpdate = new WaitForFixedUpdate();

        while (true)
        {
            yield return waitFixedUpdate;

            Vector2 moveInput = moveAction.ReadValue<Vector2>();

            // 카메라의 up, forward를 xz평면에서의 이동 방향으로 변환
            Vector3 moveAxisX = Camera.main.transform.right;
            Vector3 moveAxisY = Camera.main.transform.forward;

            moveAxisX.y = 0f;
            moveAxisY.y = 0f;

            moveAxisX.Normalize();
            moveAxisY.Normalize();

            // 입력 방향으로 가속도를 적용
            Vector3 accel = moveAccel * (moveAxisX * moveInput.x + moveAxisY * moveInput.y);
            rigidbody.AddForce(accel, ForceMode.Force);

            // 속도 상한(속도 능력치) 적용
            if (rigidbody.velocity.sqrMagnitude > model.MoveSpeed)
            {
                rigidbody.velocity = rigidbody.velocity.normalized * model.MoveSpeed;
            }
        }
    }

    private void RotatePlayerLook(InputAction.CallbackContext context)
    {
        Vector2 inputValue = context.ReadValue<Vector2>();

        // 수평 회전
        transform.Rotate(Vector3.up, inputValue.x * cameraSensitivity.x);

        // 수직 회전
        verticalCameraAngle += inputValue.y * cameraSensitivity.y;
        verticalCameraAngle = Mathf.Clamp(verticalCameraAngle, -maxVerticalCameraAngle, maxVerticalCameraAngle);
        head.localRotation = Quaternion.AngleAxis(verticalCameraAngle, Vector3.left);
    }
}
