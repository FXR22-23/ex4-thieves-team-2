using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonsterLove.StateMachine;


public class Driver
{
    public StateEvent Update;

	public StateEvent GettingHit;

	public StateEvent SeeingPlayer;
}

public class EnemyBehaviour : MonoBehaviour
{
	[SerializeField] public Transform target;

    //Declare which states we'd like use
	public enum States
	{
		Idle,
		Chasing,
		Avoiding,
		Dead
	}

	public float health = 100;
	public float damage = 20;

	public float distToTarget; 

	private float startHealth;

	[SerializeField] private float moveSpeed = 1.66f;

	Rigidbody rb;

	public StateMachine<States, Driver> fsm;

	EnemyMovement m;

	private string lastState;

    private void Awake()
	{

		startHealth = health;

		rb = GetComponent<Rigidbody>();

		//Initialize State Machine Engine		
		fsm = new StateMachine<States, Driver>(this);
		fsm.ChangeState(States.Chasing);

		m = GetComponent<EnemyMovement>();

	}

    // Start is called before the first frame update
    void Start()
    {
        this.Taunt();
    }

	IEnumerator waiter()
	{
		yield return new WaitForSeconds(5);

		if (this.lastState == "Chasing") {
			this.DeTaunt();
		} else if (this.lastState == "Avoiding") {
			this.Taunt();
		}

		m.StartMovingMesh();
		
	}

	void Update() {
		this.distToTarget = Vector3.Distance (target.position, rb.transform.position);
		if (this.distToTarget < 2 && fsm.State.ToString() != "Idle") {
			this.ToggleTaunt();
		}
		fsm.Driver.Update.Invoke();
		
	}
	void Idle_Update() {
		
	}

	public void ToggleTaunt() {
		this.lastState = fsm.State.ToString();
		fsm.ChangeState(States.Idle);
		m.StandStill();
		StartCoroutine(waiter());
	}

	public void Taunt() {
		fsm.ChangeState(States.Chasing);
	}
	public void DeTaunt() {
		fsm.ChangeState(States.Avoiding);
	}

	void Idle_Enter() {
		
	}

	void Chasing_Enter() {
		
	}

	void Chasing_Update() {
		Vector3 direction = (target.position - transform.position);
		m.MakeMove(direction, 1);
	}


	void Avoiding_Update() {
		Vector3 direction = (transform.position - target.position);
		m.MakeMoveAvoid(direction, -1);
	}
	

	// void Fighting_Hit(int strength) {
	// 	this.health = this.health-=strength;
	// }
}
