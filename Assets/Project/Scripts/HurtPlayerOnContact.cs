using UnityEngine;
using System.Collections;

public class HurtPlayerOnContact : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
//        if (other.tag.Equals("Player") || other.tag.Equals("PlayerHitbox"))
        if (other.tag.Equals("PlayerHitbox"))
        {
//            var player = other.GetComponent<PlayerController>();
            var player = other.transform.parent.GetComponent<PlayerController>();
            player.Death();
        }
    }
}