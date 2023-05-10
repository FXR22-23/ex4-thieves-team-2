using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AnimationStateControl : MonoBehaviour
{
    public Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int isGroundedHash;

    PlayerMovement m;
    // Start is called before the first frame update
    void Start()
    {
        m = GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("IsWalking");
        isRunningHash = Animator.StringToHash("IsRunning");
        isJumpingHash = Animator.StringToHash("IsJumping");
        isGroundedHash = Animator.StringToHash("IsGroundedP");

    }

    bool movementMade() {

        return Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;

    }

    // Update is called once per frame
    void Update()
    {
        bool isJumping = animator.GetBool(isJumpingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isGroundedP = animator.GetBool(isGroundedHash);
        bool forwardPressed =  movementMade();//Input.GetKey("w");
        bool runPressed = Input.GetKey("left shift");
        bool jumpPressed = Input.GetKey("space");
        animator.SetBool(isGroundedHash, m.IsGrounded());


        
        
        if (!isWalking && forwardPressed)
        {
            animator.SetBool(isWalkingHash, true);
        }
        if (isWalking && !forwardPressed)
        {
            animator.SetBool(isWalkingHash, false);
        }
        if (!isRunning && (forwardPressed && runPressed))
        {
            m.SetMoveSpeed(5f);
            animator.SetBool(isRunningHash, true);
        }
        if (isRunning && (!forwardPressed || !runPressed))
        {
            m.SetMoveSpeed(1.66f);

            animator.SetBool(isRunningHash, false);
        }
        if (jumpPressed && isGroundedP)
        {
            animator.SetBool(isJumpingHash, true);
        }
        if(!isGroundedP && !jumpPressed)
        {
            animator.SetBool(isJumpingHash, false);
        }

    }
}
