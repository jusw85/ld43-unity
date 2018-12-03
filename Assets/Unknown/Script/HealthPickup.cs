using UnityEngine;
using System.Collections;

public class HealthPickup : MonoBehaviour
{
    public int healthToGive;

    public HealthManager healthManager;


    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerController>() == null)
            return;
        healthManager.HurtPlayer(-healthToGive);

        Destroy(gameObject);
    }
}