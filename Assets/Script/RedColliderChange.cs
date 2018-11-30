using UnityEngine;
using System.Collections;

public class RedColliderChange : MonoBehaviour
{

	public PlayerController player;

	// Use this for initialization
	void Start () 
	{
		player = FindObjectOfType<PlayerController> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
	

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player_blue") 
		{
			//gameObject.GetComponent<BoxCollider2D>().enabled = true;
			Debug.Log ("blue player detected");
			GetComponent<BoxCollider2D>().isTrigger = false;
		}

		if (other.tag == "Player_red") 
		{
			//gameObject.GetComponent<BoxCollider2D>().enabled = false;
			Debug.Log ("red player detected");
			GetComponent<BoxCollider2D>().isTrigger = true;
		}
	}
}