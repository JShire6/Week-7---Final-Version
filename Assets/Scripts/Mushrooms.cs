using UnityEngine;
using System.Collections;

public class Mushrooms : MonoBehaviour 
{
	public float speed = 6.0F;
	public float gravity = 20.0F;

	private Vector3 moveDirection = Vector3.zero;

	GameObject playerGameObject; // this is a reference to the player game object

	public Vector3 direction = new Vector3(1.0f, 0.0f, 0.0f); // normalised direction the enemy will move in

	// Use this for initialization
	void Start () 
	{

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
			direction = -direction;
		} 
	}

	// Update is called once per frame
	void Update () 
	{
		// get the character controller attached to the enemy game object
		CharacterController controller = GetComponent<CharacterController>();

		// check to see if the enemy is on the ground
		if (controller.isGrounded) 
		{
			// set character controller moveDirection to be the direction I want the enemy to move in
			moveDirection = direction;
			moveDirection *= speed;
		}


		// apply gravity to movement direction
		moveDirection.y -= gravity * Time.deltaTime;

		// make the call to move the character controller
		controller.Move(moveDirection * Time.deltaTime);
	}
}

