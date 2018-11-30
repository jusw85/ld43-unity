using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
	public GameObject platform;
	public GameObject player;

	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{

	}
	
	//If character collides with the platform, make it its child.
	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			MakeChild ();   
		}
	}
	//Once it leaves the platform, become a normal object again.
	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.tag == "Player") {
			ReleaseChild(); 
		}
	}
	
	void MakeChild(){
		player.transform.parent = platform.transform;
	}
	
	void ReleaseChild(){
		player.transform.parent = null;
	}
}