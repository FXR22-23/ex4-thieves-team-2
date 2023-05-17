using UnityEngine.InputSystem;
using UnityEngine;
using System;

public class AvatarAnimationController : MonoBehaviour
{
    [SerializeField] private InputActionReference move;

    [SerializeField] private Animator animator;


    private void OnEnable() {
        move.action.started += AnimateLegs;
        move.action.canceled += StopAnimateLegs;
    }

    private void OnDisable() {

        move.action.started -= AnimateLegs;
        move.action.canceled -= StopAnimateLegs;

    }


    private void AnimateLegs(InputAction.CallbackContext context)
    {
        bool isMovingForward = move.action.ReadValue<Vector2>().y > 0;
        if (isMovingForward) {
            animator.SetBool("isWalking", true);
            //animator.SetFloat("animSpeed", 1);
        } else {
            animator.SetBool("isWalking", true);
            //animator.SetFloat("animSpeed", -1);
        }
    }

    private void StopAnimateLegs(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", false);
        //animator.SetFloat("animSpeed", 0);
    }
}
