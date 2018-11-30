using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;

    public float Damage = 10;
    //public LayerMask whatToHit;

    //public Transform BulletTrailPrefab;
    //public Transform MuzzleFlashPrefab;
    //float timeToSpawnEffect = 0;
    //public float effectSpawnRate = 10;

    //float timeToFire = 0;

    // Use this for initialization

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
    }


    //void Effect () {
    //Instantiate (BulletTrailPrefab, firePoint.position, firePoint.rotation);
    //Transform clone = Instantiate (MuzzleFlashPrefab, firePoint.position, firePoint.rotation) as Transform;
    //clone.parent = firePoint;
    //float size = Random.Range (0.6f, 0.9f);
    //clone.localScale = new Vector3 (size, size, size);
    //Destroy (clone.gameObject, 0.02f);
}