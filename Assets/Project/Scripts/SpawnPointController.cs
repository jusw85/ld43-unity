using System;
using System.Collections;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    public GameObject spawnType;
    public float initialSpawnDelay = 0.0f;
    public float defaultSpawnDelay = 2.0f;

    private void Awake()
    {
        if (initialSpawnDelay <= 0f)
            SpawnNow();
        else
            Spawn(defaultSpawnDelay);
    }

    public void Spawn()
    {
        Spawn(defaultSpawnDelay);
    }

    public void Spawn(float delay)
    {
        StartCoroutine(DoAfterSeconds(delay, SpawnNow));
    }

    private void SpawnNow()
    {
        var obj = Instantiate(spawnType, transform.position, Quaternion.identity);
        obj.GetComponent<PlayerController>().spawnPoint = this;
    }

    private IEnumerator DoAfterSeconds(float delay, Action op)
    {
        yield return new WaitForSeconds(delay);
        op();
    }
}