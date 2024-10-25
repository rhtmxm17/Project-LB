using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

/*
 * 플레이어 피격 처리 또한 플레이어 조작에서 담당하고 있음
 * 전투 관련 스크립트가 많아진다면 분리하자
 */

[RequireComponent(typeof(PlayerModel))]
public class PlayerControl : MonoBehaviour
{
    [SerializeField] PlayerInput playerInput;
    [SerializeField] Transform head;
    [SerializeField] float maxVerticalCameraAngle;
    [SerializeField] Vector2 cameraSensitivity;
    [SerializeField, Tooltip("근접 공격에 대한 무적시간")] float meleeImmunityTime = 1f;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction fireAction;
    private PlayerModel model;
    private NavMeshAgent agent;
    private float MoveSpeed => model.MoveSpeed;
    private float verticalCameraAngle;
    private bool meleeImmunity;
    private Coroutine moveRoutine;

    private void Awake()
    {
        model = GetComponent<PlayerModel>();
        agent = GetComponent<NavMeshAgent>();
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        fireAction = playerInput.actions["Fire"];
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
            agent.Move(Time.deltaTime * velocity);

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

    public void Damaged(int damage, DamageType type = 0)
    {
        if (false == meleeImmunity || type == DamageType.IGNORE_MELEE_IMMUNITY)
        {
            // 근접공격 면역 상태가 아니거나 특수 공격(원거리 등)일 경우에만 데미지 처리
            model.Hp -= damage;
            StartCoroutine(StartMeleeImmunity(meleeImmunityTime));
        }
    }

    private IEnumerator StartMeleeImmunity(float time)
    {
        meleeImmunity = true;
        yield return new WaitForSeconds(time);
        meleeImmunity = false;
    }
}
