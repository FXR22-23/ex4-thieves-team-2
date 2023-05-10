using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAnimation : MonoBehaviour
{
    private Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isJumpingHash;
    int isGroundedHash;

    private Transform parent;

    private EnemyBehaviour behaviour;

    private NavMeshAgent agent;

    EnemyMovement m;
    // Start is called before the first frame update
    void Start()
    {

        parent = GameObject.Find("Enemy").transform;

        behaviour = GetComponent<EnemyBehaviour>();

        m = GetComponent<EnemyMovement>();

        agent = GetComponent<NavMeshAgent>();
        
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("IsWalking");
        isRunningHash = Animator.StringToHash("IsRunning");
        isJumpingHash = Animator.StringToHash("IsJumping");
        isGroundedHash = Animator.StringToHash("IsGroundedP");

    }

    bool isMoving() {
        return isChasing() || isAvoiding();
    }

    bool isAvoiding() {
        return behaviour.fsm.State.ToString() == "Avoiding";
    }

    bool isChasing() {
        return behaviour.fsm.State.ToString() == "Chasing";
    }


    // Update is called once per frame
    void Update()
    {
        //bool isJumping = animator.GetBool(isJumpingHash);
        bool isRunning = animator.GetBool(isRunningHash);
        bool isWalking = animator.GetBool(isWalkingHash);
        bool isGroundedP = animator.GetBool(isGroundedHash);
        bool isMoving =  this.isMoving();
        bool isChasing =  this.isChasing();
        bool isAvoiding =  this.isAvoiding();
        //bool runPressed = Input.GetKey("left shift");
        //bool jumpPressed = Input.GetKey("space");
        animator.SetBool(isGroundedHash, true);


        
        
        if (!isWalking && isMoving)
        {
            animator.SetBool(isWalkingHash, true);
            agent.speed = 1;
            //animator.SetBool(isRunningHash, true);
        }

        if (isWalking && (isChasing || (isAvoiding && behaviour.distToTarget < 6)))
        {
            animator.SetBool(isRunningHash, true);
            agent.speed = 1.5f;
        }

        if (isWalking && (isAvoiding && behaviour.distToTarget >= 6)) {
            animator.SetBool(isRunningHash, false);
        }

        if (isWalking && (!isMoving))
        {
            animator.SetBool(isWalkingHash, false);
            animator.SetBool(isRunningHash, false);
        }
        //if (!isRunning && (forwardPressed && runPressed))
        //{
        //    m.SetMoveSpeed(5f);
        //    animator.SetBool(isRunningHash, true);
        //}
        //if (isRunning && (!forwardPressed || !runPressed))
        //{
        //    m.SetMoveSpeed(1.66f);

        //    animator.SetBool(isRunningHash, false);
        //}
        //if (jumpPressed && isGroundedP)
        //{
        //    animator.SetBool(isJumpingHash, true);
        //}
        //if(!isGroundedP && !jumpPressed)
        //{
        //    animator.SetBool(isJumpingHash, false);
        //}

    }
}
