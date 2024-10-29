using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerModel), typeof(CharacterController))]
public class PlayerCharacterControllerControl : MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] float maxVerticalCameraAngle = 70f;
    [SerializeField] Vector2 cameraSensitivity = Vector2.one * 0.5f;

    private InputAction moveAction;
    private InputAction lookAction;
    private PlayerModel model;
    private CharacterController controller;
    private float MoveSpeed => model.MoveSpeed;
    private float verticalCameraAngle;
    private Coroutine moveRoutine;

    private void Awake()
    {
        model = GetComponent<PlayerModel>();
        controller = GetComponent<CharacterController>();

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
        moveRoutine = StartCoroutine(MovementRoutine());
        //moveAction.started += MoveActionStarted;
        //moveAction.canceled += MoveActionCanceled;
        lookAction.performed += RotatePlayerLook;
    }

    private void OnDisable()
    {
        StopCoroutine(moveRoutine);
        //moveAction.started -= MoveActionStarted;
        //moveAction.canceled -= MoveActionCanceled;
        lookAction.performed -= RotatePlayerLook;
    }

    private void MoveActionStarted(InputAction.CallbackContext _) => moveRoutine = StartCoroutine(MovementRoutine());

    private void MoveActionCanceled(InputAction.CallbackContext _) => StopCoroutine(moveRoutine);

    private IEnumerator MovementRoutine()
    {
        while (true)
        {
            Vector2 moveInput = moveAction.ReadValue<Vector2>();

            // 카메라의 up, forward를 xz평면에서의 이동 방향으로 변환
            Vector3 moveAxisX = Camera.main.transform.right;
            Vector3 moveAxisY = Camera.main.transform.forward;

            moveAxisX.y = 0f;
            moveAxisY.y = 0f;

            moveAxisX.Normalize();
            moveAxisY.Normalize();

            // 입력 방향과 속도 능력치를 적용
            Vector3 velocity = MoveSpeed * (moveAxisX * moveInput.x + moveAxisY * moveInput.y);

            controller.SimpleMove(velocity);

            yield return null;
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
