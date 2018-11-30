using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject currentCheckpoint;

	private PlayerController player;

	public GameObject deathParticle;
	public GameObject respawnParticle;

	public int pointPenaltyOnDeath;

	public float respawnDelay;

	private float gravityStore;

	private Camera2DFollow camera;

	public HealthManager healthManager;

	//cache
	private AudioManager audioManager;

	// Use this for initialization
	void Start () 
	{
		player = FindObjectOfType<PlayerController> ();

		healthManager = FindObjectOfType<HealthManager>();

		camera = FindObjectOfType<Camera2DFollow> ();

		//caching
		audioManager = AudioManager.instance;
		if (audioManager == null) 
		{
			Debug.LogError ("No audio Manager found");
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	public void RespawnPlayer ()
	{
		StartCoroutine ("RespawnPlayerCo");
	}

	public IEnumerator RespawnPlayerCo()
	{
		audioManager.PlaySound ("Respawn");

		camera.enabled = false;
		Instantiate (deathParticle, player.transform.position, player.transform.rotation);
		player.enabled = false;
		player.GetComponent<Renderer> ().enabled = false;
		gravityStore = player.GetComponent<Rigidbody2D> ().gravityScale;
		player.GetComponent<Rigidbody2D> ().gravityScale = 0f;
		ScoreManager.AddPoints (-pointPenaltyOnDeath);
		Debug.Log ("Player Respawn");
		yield return new WaitForSeconds (respawnDelay);
		camera.enabled = true;
		player.GetComponent<Rigidbody2D> ().gravityScale = 2f;
		player.transform.position = currentCheckpoint.transform.position;
		player.knockbackCount = 0;
		player.enabled = true;
		player.GetComponent<Renderer> ().enabled = true;
		healthManager.FullHealth ();
		healthManager.isDead = false;
		Instantiate (respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
	}
	}
