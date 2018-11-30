﻿using UnityEngine;
using System.Collections;

public class ZombieEnemy_Patrol : MonoBehaviour
{
    public Transform[] patrolpoints;
    int currentPoint;
    public float speed = 0.5f;
    public float timestill = 2f;
    public float sight = 3f;
    public float force;

    Animator anim;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("Patrol");
        anim.SetBool("walking", true);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, sight);
        if (hit.collider != null && hit.collider.tag == "Player_red")
            GetComponent<Rigidbody2D>()
                .AddForce(Vector3.up * force + (hit.collider.transform.position - transform.position) * force);
    }

    IEnumerator Patrol()
    {
        while (true)
        {
            if (transform.position.x == patrolpoints[currentPoint].position.x)
            {
                currentPoint++;
                anim.SetBool("walking", false);
                yield return new WaitForSeconds(timestill);
                anim.SetBool("walking", true);
            }

            if (currentPoint >= patrolpoints.Length)
            {
                currentPoint = 0;
            }

            transform.position = Vector2.MoveTowards(transform.position,
                new Vector2(patrolpoints[currentPoint].position.x, transform.position.y), speed);

            if (transform.position.x > patrolpoints[currentPoint].position.x)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (transform.position.x < patrolpoints[currentPoint].position.x)
                transform.localScale = new Vector3(1, 1, 1);

            yield return null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * sight);
    }
}