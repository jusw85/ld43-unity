using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb_Controller : MonoBehaviour
{
    //public float speed;
    public PlayerController player;
    public GameObject enemyDeathEffect;
    public GameObject impactEffect;

    public int pointsForKill;

    //public float rotationSpeed;
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

        //if (player.transform.localScale.x < 0) 
        //{
        //GetComponent<SpriteRenderer> ().flipX = true;
        //speed = -speed;
        //rotationSpeed = -rotationSpeed;
        //}

        //else
        //GetComponent<SpriteRenderer> ().flipX = false;
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Rigidbody2D>().velocity = new Vector2 (speed, GetComponent<Rigidbody2D>().velocity.y);

        //GetComponent<Rigidbody2D> ().angularVelocity = rotationSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy_blue")
        {
            other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 1), ForceMode2D.Impulse);
        }


        if (other.tag == "Destructable_blue")
        {
            other.GetComponent<Destroy_Block>().giveDamage(damageToGive);
        }
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