using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour 
{
	public GameObject bullet;
	public float shotDelay = 0.2f;
	private bool readytoShoot = true;

	// Use this for initialization
	void Start () 
	{
		Invoke ("Shoot", shotDelay);
	}
	
	// Update is called once per frame
	void Shoot()
	{
		Instantiate (bullet, transform.position, transform.rotation);
		Invoke ("Shoot", shotDelay);
	}
}
