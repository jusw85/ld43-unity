using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Block : MonoBehaviour
{
    public int blockHealth;

    public GameObject deathEffect;

    private SpriteRenderer spriteRenderer;

    public Sprite Block3;
    public Sprite Block2;
    public Sprite Block1;
    public Sprite Block0;

    public int pointsOnDeath;

    //cache
    private AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        //caching
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio Manager found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (blockHealth == 3)
        {
            spriteRenderer.sprite = Block3;
        }

        if (blockHealth == 2)
        {
            spriteRenderer.sprite = Block2;
        }

        if (blockHealth == 1)
        {
            spriteRenderer.sprite = Block1;
        }

        if (blockHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            ScoreManager.AddPoints(pointsOnDeath);
            Destroy(gameObject);
            GetComponent<Collider2D>().enabled = false;
            //audioManager.PlaySound ("deathBlockSpawn");
        }
    }

    public void giveDamage(int damageToGive)
    {
        blockHealth -= damageToGive;
    }
}