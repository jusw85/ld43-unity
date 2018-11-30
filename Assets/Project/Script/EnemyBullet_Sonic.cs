﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet_Sonic : MonoBehaviour
{
    public float speed;

    public PlayerController player;

    public GameObject impactEffect;

    public float rotationSpeed;

    public int damageToGive;

    private Rigidbody2D myrigidbody2D;

    public HealthManager healthManager;


    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        myrigidbody2D = GetComponent<Rigidbody2D>();

        healthManager = FindObjectOfType<HealthManager>();

        if (player.transform.position.x < transform.position.x)
        {
            speed = -speed;
            rotationSpeed = -rotationSpeed;
        }
    }

    void Update()
    {
        myrigidbody2D.velocity = new Vector2(speed, myrigidbody2D.velocity.y);

        myrigidbody2D.angularVelocity = rotationSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player_blue")
        {
            Debug.Log("Player Hit");
            healthManager.HurtPlayer(damageToGive);
        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}