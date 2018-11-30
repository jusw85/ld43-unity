using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BulletVertical : MonoBehaviour
{
    public PlayerController player;

    public float speed = 5f;

    public GameObject impactEffect;

    private Rigidbody2D rb;

    public HealthManager healthManager;

    public int damageToGive;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        healthManager = FindObjectOfType<HealthManager>();

        player = FindObjectOfType<PlayerController>();
    }


    void FixedUpdate()
    {
        rb.velocity = transform.up * speed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player_blue")
        {
            healthManager.HurtPlayer(damageToGive);
        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}