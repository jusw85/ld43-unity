using UnityEngine;
using System.Collections;

public class BuildingBlockBullet : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bullet;

    private float spawnDelay = 0.2f;

    // Use this for initialization
    void Start()
    {
        Invoke("spawnBullet", spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, spawnDelay);
    }

    void spawnBullet()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}