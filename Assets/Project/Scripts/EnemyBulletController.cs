using UnityEngine;
using System.Collections;

public class EnemyBulletController : MonoBehaviour
{
    public float rotationSpeed;
    public GameObject impactEffect;

    [System.NonSerialized]
    public float speed;

    private Rigidbody2D rb2d;

    private void Start()
    {
//        PlayerController player = FindObjectOfType<PlayerController>();
//        if (player != null && player.transform.position.x < transform.position.x)
//        {
//            speed = -speed;
//            rotationSpeed = -rotationSpeed;
//        }

        rb2d = GetComponent<Rigidbody2D>();
        UpdateRb2d();
    }

//    private void FixedUpdate()
//    {
//        UpdateRb2d();
//    }

    private void UpdateRb2d()
    {
//        rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
//        rb2d.angularVelocity = rotationSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}