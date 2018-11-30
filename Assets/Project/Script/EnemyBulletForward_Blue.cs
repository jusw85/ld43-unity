using UnityEngine;
using System.Collections;

public class EnemyBulletForward_Blue : MonoBehaviour 
{

	public float speed = 1.0f;
	public PlayerController player;

	public GameObject impactEffect;

	public int damageToGive;

	private Rigidbody2D myrigidbody2D;

	public HealthManager healthManager;


	// Use this for initialization
	void Start () 
	{
		//player = FindObjectOfType<PlayerController> ();

		myrigidbody2D = GetComponent<Rigidbody2D> ();

		healthManager = FindObjectOfType<HealthManager>();

	}
	
	// Update is called once per frame
	void Update () 
	{
		//transform.position += transform.forward * speed * Time.deltaTime;

		myrigidbody2D.velocity = new Vector2 (speed, myrigidbody2D.velocity.y);
	
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player_red") 
		{
			Debug.Log ("Player Hit");
			healthManager.HurtPlayer(damageToGive);
		}

		Instantiate (impactEffect, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}
