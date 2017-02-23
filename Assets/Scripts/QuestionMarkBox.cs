using UnityEngine;
using System.Collections;

public class QuestionMarkBox : MonoBehaviour 
{

	public GameObject pick_up;

	bool pick_up_spawned;
	Vector3 spawn_location;

	// Use this for initialization
	void Start () 
	{
		spawn_location = new Vector3 (this.transform.position.x, this.transform.position.y + 1.7f, 0.0f);
		pick_up_spawned = false;
	}

	void SpawnPickUp()
	{
		if (pick_up_spawned == false) 
		{
			pick_up = Instantiate (pick_up, spawn_location, Quaternion.identity) as GameObject;
			pick_up_spawned = true;
		}
	}

	// Update is called once per frame
	void Update () 
	{
	
	}
}
