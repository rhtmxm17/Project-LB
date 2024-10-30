using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Renderer meshRender;

    private PlayerCharacterControllerControl locomotionControl;
    private int hashMoveSpeed;
    private int hashIsMoving;

    public bool ShadowOnly
    {
        set
        {
            meshRender.shadowCastingMode = value ? ShadowCastingMode.ShadowsOnly : ShadowCastingMode.On;
        }
    }

    private void Awake()
    {
        locomotionControl = GetComponent<PlayerCharacterControllerControl>();
        hashMoveSpeed = Animator.StringToHash("MoveSpeed");
        hashIsMoving = Animator.StringToHash("IsMoving");
    }

    private void Update()
    {
        animator.SetFloat(hashMoveSpeed, locomotionControl.Velocity.magnitude);
        animator.SetBool(hashIsMoving, locomotionControl.IsMoving);
    }
}
