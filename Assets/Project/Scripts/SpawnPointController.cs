using UnityEngine;
using System.Collections;

public class SpawnPointController : MonoBehaviour {

    public GameObject spawnType;

    private void Awake() {
        var obj = Instantiate(spawnType, transform.position, Quaternion.identity);
//        obj.GetComponent<DudeController>().respawnPoint = transform.position;
    }

}
