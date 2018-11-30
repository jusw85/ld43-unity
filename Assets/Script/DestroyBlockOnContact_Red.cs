using UnityEngine;
using System.Collections;

public class DestroyBlockOnContact_Red : MonoBehaviour 
{

	public int blockHealth;

	public GameObject deathEffect;

	public int pointsOnDeath;

	//cache
	private AudioManager audioManager;

	// Use this for initialization
	void Start () 
	{
		//caching
		audioManager = AudioManager.instance;
		if (audioManager == null) {
			Debug.LogError ("No audio Manager found");
		}

	}

	// Update is called once per frame
	void Update () {

		if (blockHealth <= 0)
		{
			Instantiate(deathEffect, transform.position, transform.rotation);
			ScoreManager.AddPoints(pointsOnDeath);
			Destroy(gameObject);
			audioManager.PlaySound ("deathBlockSpawn");
		}

	}

	public void giveDamage(int damageToGive)
	{
		blockHealth -= damageToGive;
	}
}
