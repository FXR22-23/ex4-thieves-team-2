using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;

    public float rotationSpeed;
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    AnimationStateControl asc ;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        asc = GetComponent<AnimationStateControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
        //var rb = GetComponent<Rigidbody>();
        if (asc.animator.GetCurrentAnimatorStateInfo(0).IsName("LandAnim")) {
            rb.velocity = Vector3.zero;
            return;
        }
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(horizontalInput * moveSpeed, rb.velocity.y, verticalInput * moveSpeed);

        
        var movementVector = new Vector3(horizontalInput, 0, verticalInput);
        movementVector.Normalize(); //rb.velocity.y

        if (movementVector != Vector3.zero) {
            Quaternion toRotation = Quaternion.LookRotation(movementVector, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed*Time.deltaTime);
        }
        

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
        //jumpSound.Play();
    }

    public void SetMoveSpeed(System.Single speed) {
        this.moveSpeed = speed;
    }

    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }
} 
