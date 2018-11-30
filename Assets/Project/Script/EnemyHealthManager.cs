using UnityEngine;
using System.Collections;

public class EnemyHealthManager : MonoBehaviour
{
    public int enemyHealth;

    public GameObject deathEffect;


    public int pointsOnDeath;

    public Sprite EnemyBlue_3;
    public Sprite EnemyBlue_2;
    public Sprite EnemyBlue_1;
    public Sprite EnemyBlue_0;

    private SpriteRenderer spriteRenderer;

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
        if (enemyHealth == 3)
        {
            spriteRenderer.sprite = EnemyBlue_3;
        }

        if (enemyHealth == 2)
        {
            spriteRenderer.sprite = EnemyBlue_2;
        }

        if (enemyHealth == 1)
        {
            spriteRenderer.sprite = EnemyBlue_1;
        }

        if (enemyHealth <= 0)
        {
            Instantiate(deathEffect, transform.position, transform.rotation);
            ScoreManager.AddPoints(pointsOnDeath);
            Destroy(gameObject);
            audioManager.PlaySound("deathBlockSpawn");

            //GetComponent<Collider2D> ().enabled = false; 
        }
    }

    public void giveDamage(int damageToGive)
    {
        enemyHealth -= damageToGive;
    }
}