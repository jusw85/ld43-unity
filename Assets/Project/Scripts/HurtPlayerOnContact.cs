﻿using UnityEngine;
using System.Collections;

public class HurtPlayerOnContact : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            var player = other.GetComponent<PlayerController>();
            player.Death();
        }
    }
}