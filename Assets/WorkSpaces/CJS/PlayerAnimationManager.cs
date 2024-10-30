using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] Renderer meshRender;

    private PlayerCharacterControllerControl locomotionControl;
    private StagePlayerControl attackControl;

    private int hashMoveSpeed;
    private int hashIsMoving;
    private int hashOnAttack;

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
        if (TryGetComponent(out attackControl))
        {
            attackControl.OnAttack += TriggerAttackAnimation;
        }
        hashMoveSpeed = Animator.StringToHash("MoveSpeed");
        hashIsMoving = Animator.StringToHash("IsMoving");
        hashOnAttack = Animator.StringToHash("OnAttack");
    }

    private void TriggerAttackAnimation()
    {
        Debug.Log("TriggerAttackAnimation");
        animator.SetTrigger(hashOnAttack);
    }

    private void Update()
    {
        animator.SetFloat(hashMoveSpeed, locomotionControl.Velocity.magnitude);
        animator.SetBool(hashIsMoving, locomotionControl.IsMoving);
    }
}
