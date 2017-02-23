using UnityEngine;
using System.Collections;

public class FireBall : MonoBehaviour 
{

	public float speed = 6.0F;
	public float gravity = 20.0F;
	public int bounce_height;
	int direc;

	private Vector3 moveDirection = new Vector3 (1.0f, 0.0f, 0.0f);

	GameObject playerGameObject; // this is a reference to the player game object

	public Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f); // normalised direction the enemy will move in

	CharacterController controller;

	// Use this for initialization
	void Start () 
	{
		controller = GetComponent<CharacterController>();
		direc = 0;
		Invoke ("kill", 2.0f);
	}

	//
	// This function is called when a CharacterController moves into an object
	//
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		// find out what we've hit
		if (hit.collider.gameObject.CompareTag ("Pipe")) 
		{
			// we've hit the pipe

			// flip the direction of the enemy
			moveDirection.x *= -1;
		}
		else if(hit.gameObject.tag == "Enemy")
		{
			Destroy (hit.gameObject);
			Destroy (this.gameObject);
		}
	}

	void kill()
	{
		//this.gameObject.SetActive (false);
		Destroy (this.gameObject);
	}

	void SetDirection (int direc)
	{
		moveDirection.x = direc * speed;
	}

	// Update is called once per frame
	void Update () 
	{
		Physics.IgnoreLayerCollision (11, 11);

		//moveDirection = direction;

		if (controller.isGrounded) 
		{
			moveDirection.y = bounce_height;
		}



		// apply gravity to movement direction
		moveDirection.y -= 2 * gravity * Time.deltaTime;

		//moveDirection.x *= speed;

		// make the call to move the character controller
		controller.Move(moveDirection * Time.deltaTime);
	}
}