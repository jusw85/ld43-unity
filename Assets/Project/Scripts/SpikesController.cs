using UnityEngine;
using System.Collections;

public class SpikesController : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.tag.Equals("Player")) {
            other.GetComponent<PlayerController>().Death();
        }
    }

}
