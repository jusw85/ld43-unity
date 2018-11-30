using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKnockBack : MonoBehaviour {





	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{

		if (other.tag == "Player_blue") 
		{
			other.GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0, 1), ForceMode2D.Impulse);
	
		}
	}
}
