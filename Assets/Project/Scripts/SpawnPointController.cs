using System;
using System.Collections;
using UnityEngine;

public class SpawnPointController : MonoBehaviour
{
    public GameObject spawnType;

    private void Awake()
    {
        SpawnNow();
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