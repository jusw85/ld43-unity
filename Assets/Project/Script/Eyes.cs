using UnityEngine;
using System.Collections;

public class Eyes : MonoBehaviour {

	GameObject player;
	Transform playerTrans;

	public Ray playerPos;

	// Use this for initialization
	void Start () 
	{
		player = GameObject.Find ("Player");
		playerTrans = player.transform;
	}
	
	// Update is called once per frame
	void Update () 
	{
		playerPos = new Ray (transform.position, transform.forward);
		Debug.DrawRay (playerPos.origin, playerPos.direction * 1, Color.red);
		transform.LookAt (playerTrans);
	}
}
