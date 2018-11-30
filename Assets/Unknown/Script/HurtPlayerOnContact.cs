﻿using UnityEngine;
using System.Collections;

public class HurtPlayerOnContact : MonoBehaviour
{
    public int damageToGive;

    public HealthManager healthManager;

    // Use this for initialization
    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            healthManager.HurtPlayer(damageToGive);

            //var player = other.GetComponent<PlayerController> ();
            //player.knockbackCount = player.knockbackLength;

            //if (other.transform.position.x < transform.position.x)
            //player.knockFromRight = true;
            //else
            //player.knockFromRight = false;
        }
    }
}