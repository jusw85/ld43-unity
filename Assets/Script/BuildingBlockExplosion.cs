using UnityEngine;
using System.Collections;

public class BuildingBlockExplosion : MonoBehaviour {

	public Transform explosionPoint;
	public GameObject explosion;

	private float spawnDelay= 1.0f;

	// Use this for initialization
	void Start () {

		Invoke ("spawnExplosion", spawnDelay);
	}

	// Update is called once per frame
	void Update () {

		Destroy (gameObject, spawnDelay);
	}

	void spawnExplosion()
	{
		Instantiate (explosion, explosionPoint.position, explosionPoint.rotation);
	}

}