using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner_Trigger : MonoBehaviour
{
    public GameObject objectToSpawn;
    //public float spawnDelay = 10.0f;

    // Use this for initialization

    void OnTriggerEnter2D(Collider2D other)

    {
        if (other.tag == "Boss2_BombSpawn")
        {
            Instantiate(objectToSpawn, transform.position, transform.rotation);
        }
    }

    //void Start () 
    //{
    //Invoke ("Spawn", spawnDelay);
    //}

    //void Spawn () 
    //{
    //Instantiate (objectToSpawn, transform.position, transform.rotation);
    //Invoke ("Spawn", spawnDelay);
    //}
}