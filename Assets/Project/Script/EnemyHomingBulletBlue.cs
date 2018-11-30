using UnityEngine;
using System.Collections;

public class EnemyHomingBulletBlue : MonoBehaviour 
{
	public float speed;

	public PlayerController player;

	public GameObject impactEffect;

	public float rotationSpeed;

	public int damageToGive;

	public string searchTag;

	private GameObject closestPlayer;
	private Transform target;

	private Rigidbody2D rb2d;

	public HealthManager healthManager;

	// Use this for initialization
	void Start() 
	{
		rb2d = GetComponent<Rigidbody2D>();
		player = FindObjectOfType<PlayerController>();

		healthManager = FindObjectOfType<HealthManager>();

		rotationSpeed = -rotationSpeed;

		closestPlayer = FindClosestPlayer();

		if (closestPlayer)
			target = player.transform;
		else
			transform.LookAt(transform.position + Vector3.forward,player.transform.localScale.x < 0 ? Vector3.left : Vector3.right);
	}

	void FixedUpdate() 
	{
		//rb2d.AddRelativeForce(Vector3.up * speed);
		transform.position = Vector2.MoveTowards (new Vector2 (transform.position.x, transform.position.y), player.transform.position, 3 * Time.deltaTime);


	}

	// Update is called once per frame
	void Update() {
		if (target == null) 
		{
			closestPlayer = FindClosestPlayer();
			if (closestPlayer) 
			{
				target = player.transform;
			}
		}
		else 
		{
			transform.LookAt(transform.position + Vector3.forward, target.position - transform.position);
		}
	}

	GameObject FindClosestPlayer() 
	{
		GameObject[] objs;
		objs =  GameObject.FindGameObjectsWithTag("Player_blue");

		GameObject closest = null;
		float distance = Mathf.Infinity;

		Vector3 position = transform.position;

		foreach (GameObject obj in objs) 
		{
			Vector3 diff = obj.transform.position - position;
			float curDistance = diff.sqrMagnitude;

			if (curDistance < distance) {
				closest = obj;
				distance = curDistance;
			}
		}
		return closest;
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
