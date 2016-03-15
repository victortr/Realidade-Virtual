using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	protected CharacterController control;
	protected Vector3 move = Vector3.zero;

	public Slider energySlider;

	public GameObject head;
	public float jumpSpeed = 8.0F;
	public float gravity = 10.0F;
	public float walkSpeed = 6f;
	public float runSpeed = 9f;
	public float speed;
	public float turnSpeed = 90f;
	public int timer = 500;

	Vector3 movement;                   // The vector to store the direction of the player's movement.
	Animator anim;                      // Reference to the animator component.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.

	public bool jump        = false;

	private Vector3 moveDirection = Vector3.zero;
	private Vector3 v3gravity;

	void Start () 
	{
		v3gravity = new Vector3 (0, gravity, 0);
	}

	void Awake ()
	{
		anim = GetComponent <Animator> ();
		playerRigidbody = GetComponent <Rigidbody> ();
	}
	
	void Update ()
	{
		//CharacterController controller = GetComponent<CharacterController>();
		// Store the input axes.
		float h = Input.GetAxisRaw ("Horizontal");
		float v = Input.GetAxisRaw ("Vertical");


		if (Input.GetButtonDown("Jump") && (timer > 250))
		{
			jump = false;
			transform.position = transform.position + (transform.up * jumpSpeed * Time.deltaTime);
			timer = timer - 250;
		} 

		if (timer < 500)
			timer = timer + 1;


		if (Input.GetButton ("Fire3") && (timer > 30)) {
			speed = runSpeed;
			timer = timer - 11;
		}
		else
			speed = walkSpeed;

		//GetComponent<Rigidbody>().AddForce (v3gravity);

		//playerRigidbody.MovePosition (transform.position);

		// Move the player around the scene.
		Move (h, v);
		
		// Turn the player to face the mouse cursor.
		Turning ();

	}
	
	void Move (float h, float v)
	{
		// Set the movement vector based on the axis input.

		Vector3 desiredMove = transform.forward*v + transform.right*h;

		movement.Set (desiredMove.x, 0f, desiredMove.z );
		
		// Normalise the movement vector and make it proportional to the speed per second.
		movement = movement.normalized * speed * Time.deltaTime;
		
		// Move the player to it's current position plus the movement.
		playerRigidbody.MovePosition (transform.position + movement);
	}

	void Turning ()
	{

		transform.rotation = head.transform.rotation; //+ offset;
		GetComponent<Rigidbody>().MoveRotation(transform.rotation);

	}

}