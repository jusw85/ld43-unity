using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float spawnDelay = 10.0f;

    // Use this for initialization
    void Start()
    {
        Invoke("Spawn", spawnDelay);
    }

    void Spawn()
    {
        Instantiate(objectToSpawn, transform.position, transform.rotation);
        Invoke("Spawn", spawnDelay);
    }
}