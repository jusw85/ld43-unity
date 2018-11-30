using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dashBullet : MonoBehaviour
{
    public float speed;
    public PlayerController player;
    public GameObject enemyDeathEffect;
    public GameObject impactEffect;
    public int pointsForKill;
    public int damageToGive;

    private Sprite defaultSprite;
    public Sprite muzzleFlash;

    public int framesToFlash = 3;
    public float destroyTime = 3;

    private SpriteRenderer spriteRend;

    // Use this for initialization
    void Start()
    {
        spriteRend = GetComponent<SpriteRenderer>();
        defaultSprite = spriteRend.sprite;

        StartCoroutine(FlashMuzzleFlash());
        StartCoroutine(TimedDestruction());

        player = FindObjectOfType<PlayerController>();

        if (player.transform.localScale.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        else
            GetComponent<SpriteRenderer>().flipX = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy_blue")
        {
            other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    IEnumerator FlashMuzzleFlash()
    {
        spriteRend.sprite = muzzleFlash;
        for (int i = 0; i < framesToFlash; i++)
        {
            yield return 0;
        }

        spriteRend.sprite = defaultSprite;
    }

    IEnumerator TimedDestruction()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}