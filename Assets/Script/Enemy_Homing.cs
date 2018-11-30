using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Homing : MonoBehaviour 
{

	public PlayerController player;

	public Transform target;

	public float speed =5f;

	public float rotateSpeed = 200f;

	public GameObject impactEffect;

	private Rigidbody2D rb;

	public HealthManager healthManager;

	public int damageToGive;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody2D> ();

		healthManager = FindObjectOfType<HealthManager>();

		player = FindObjectOfType<PlayerController>();
	
		target = player.transform;
	}
	

	void FixedUpdate () 
	{
		Vector2 direction = (Vector2)target.position - rb.position;

		direction.Normalize ();

		float rotateAmount = Vector3.Cross (direction, transform.up).z;

		rb.angularVelocity = -rotateAmount * rotateSpeed;

		rb.velocity = transform.up * speed;

	}

	void OnTriggerEnter2D(Collider2D other) 
	{
		if (other.tag == "Player_blue") 
		{
			healthManager.HurtPlayer(damageToGive);
		}

		Instantiate(impactEffect, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
