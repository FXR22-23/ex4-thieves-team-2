
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody rb;

    public float rotationSpeed;

    private Vector3 helper;

    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce = 5f;

    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    AnimationStateControl asc ;

    [SerializeField] public Transform destination;
    private NavMeshAgent agent;
    private NavMeshPath path;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        asc = GetComponent<AnimationStateControl>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    

    public void MakeMove(Vector3 direction, int isChasing) {
        helper = agent.desiredVelocity;//new Vector3(direction.x, 0, direction.z) * moveSpeed;
        Quaternion toRotation = Quaternion.LookRotation(helper, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        if (!agent.SetDestination(transform.position + isChasing * direction)) {
            Debug.Log("ERROR");
        }
        //path = new NavMeshPath();
        //NavMesh.CalculatePath(transform.position, destination.position, NavMesh.AllAreas, path);
    }


    public void MakeMoveAvoid(Vector3 direction, int isChasing) {
        helper = agent.desiredVelocity;//new Vector3(direction.x, 0, direction.z) * moveSpeed;
        Quaternion toRotation = Quaternion.LookRotation(helper, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        this.SetDest(transform.position - isChasing * direction);
        //path = new NavMeshPath();
        //NavMesh.CalculatePath(transform.position, destination.position, NavMesh.AllAreas, path);
    }

    public void SetDest(Vector3 position) {
        if (!agent.SetDestination(position)) {
            Debug.Log("ERROR");
        }
    }

    void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
    }

    public void SetMoveSpeed(System.Single speed) {
        this.moveSpeed = speed;
    }


    public void StandStill() {
        agent.isStopped = true;
        rb.velocity = Vector3.zero;
    }

    public void StartMovingMesh() {
        agent.isStopped = false;
    }

    public bool IsGrounded()
    {
        return Physics.CheckSphere(groundCheck.position, .1f, ground);
    }
} 
