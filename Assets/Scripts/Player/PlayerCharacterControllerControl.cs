using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

[RequireComponent(typeof(PlayerModel), typeof(CharacterController))]
public class PlayerCharacterControllerControl : MonoBehaviour
{
    [SerializeField] Transform head;
    [SerializeField] float maxVerticalCameraAngle = 70f;
    [SerializeField] Vector2 cameraSensitivity = Vector2.one * 0.5f;
    [SerializeField, Tooltip("달리기 배율")] float runPow = 2f;

    public bool IsMoving { get; private set; }
    public Vector3 Velocity => controller.velocity; // 실시간 이동중인 속도

    private InputAction moveAction; // 이동 입력
    private InputAction lookAction; // 카메라 회전 입력
    private InputAction runAction; // 달리기 입력
    private InputAction cursorAction; // 커서 보이기/숨기기 입력
    private PlayerModel model;
    private CharacterController controller;
    private float MoveSpeed => model.MoveSpeed; // 능력치로서의 이동 속도
    private float verticalCameraAngle;
    private Coroutine moveRoutine;
    private bool isRunning = false;
    private bool rotateLook = true;

    /// <summary>
    /// 마우스 잠금 및 카메라 회전 활성 상테를 설정합니다<br/>
    /// </summary>
    /// <param name="lockEnable">true일 경우 마우스를 잠그고 카메라 회전 적용</param>
    public void MouseLock(bool lockEnable)
    {
        if (lockEnable)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (! rotateLook)
                lookAction.performed += RotatePlayerLook;
            rotateLook = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (rotateLook)
                lookAction.performed -= RotatePlayerLook;
            rotateLook = false;
        }
    }

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
        runAction = playerInput.actions["Run"];
        cursorAction = playerInput.actions["ActiveCursor"];
    }

    private void OnEnable()
    {
        moveRoutine = StartCoroutine(MovementRoutine());
        //moveAction.started += MoveActionStarted;
        //moveAction.canceled += MoveActionCanceled;
        lookAction.performed += RotatePlayerLook;
        runAction.started += RunActionUpdate;
        runAction.canceled += RunActionUpdate;
        cursorAction.started += ToggleActiveCursor;
    }

    private void OnDisable()
    {
        StopCoroutine(moveRoutine);
        //moveAction.started -= MoveActionStarted;
        //moveAction.canceled -= MoveActionCanceled;
        lookAction.performed -= RotatePlayerLook;
        runAction.started -= RunActionUpdate;
        runAction.canceled -= RunActionUpdate;
        cursorAction.started -= ToggleActiveCursor;
    }

    private void RunActionUpdate(InputAction.CallbackContext context)
    {
        isRunning = context.started;
    }

    private void MoveActionStarted(InputAction.CallbackContext _) => moveRoutine = StartCoroutine(MovementRoutine());

    private void MoveActionCanceled(InputAction.CallbackContext _) => StopCoroutine(moveRoutine);

    private void ToggleActiveCursor(InputAction.CallbackContext _) => MouseLock(! rotateLook);

    private IEnumerator MovementRoutine()
    {
        while (true)
        {
            Vector2 moveInput = moveAction.ReadValue<Vector2>();

            IsMoving = (moveInput != Vector2.zero);
            if (IsMoving)
            {
                // 카메라의 up, forward를 xz평면에서의 이동 방향으로 변환
                Vector3 moveAxisX = Camera.main.transform.right;
                Vector3 moveAxisY = Camera.main.transform.forward;

                moveAxisX.y = 0f;
                moveAxisY.y = 0f;

                moveAxisX.Normalize();
                moveAxisY.Normalize();

                // 입력 방향과 속도 능력치를 적용
                float speed = MoveSpeed;
                if (isRunning)
                    speed *= runPow;
                Vector3 velocity = speed * (moveAxisX * moveInput.x + moveAxisY * moveInput.y);

                controller.SimpleMove(velocity);
            }
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
