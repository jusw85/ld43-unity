using UnityEngine;
using System.Collections;

public class NPCMovement : MonoBehaviour
{
    public float moveSpeed;
    private Vector2 minWalkPoint;
    private Vector2 maxWalkPoint;

    private Rigidbody2D myRigidbody;

    public bool isWalking;

    public float walkTime;
    private float walkCounter;
    public float waitTime;
    private float waitCounter;

    private int WalkDirection;

    public Collider2D walkZone;
    private bool hasWalkZone;

    // Use this for initialization
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();

        waitCounter = waitTime;
        walkCounter = walkTime;

        ChooseDirection();

        if (walkZone != null)
        {
            minWalkPoint = walkZone.bounds.min;
            maxWalkPoint = walkZone.bounds.max;
            hasWalkZone = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isWalking)
        {
            walkCounter -= Time.deltaTime;


            switch (WalkDirection)
            {
                case 0:
                    myRigidbody.velocity = new Vector2(0, 0);
                    break;

                case 1:
                    myRigidbody.velocity = new Vector2(moveSpeed, 0);
                    GetComponent<SpriteRenderer>().flipX = true;
                    if (hasWalkZone && transform.position.x > maxWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }

                    break;

                case 2:
                    myRigidbody.velocity = new Vector2(0, 0);
                    break;

                case 3:
                    myRigidbody.velocity = new Vector2(-moveSpeed, 0);
                    GetComponent<SpriteRenderer>().flipX = false;
                    if (hasWalkZone && transform.position.x < minWalkPoint.x)
                    {
                        isWalking = false;
                        waitCounter = waitTime;
                    }

                    break;
            }

            if (walkCounter < 0)
            {
                isWalking = false;
                waitCounter = waitTime;
            }
        }


        else
        {
            waitCounter -= Time.deltaTime;

            myRigidbody.velocity = Vector2.zero;

            if (waitCounter < 0)
            {
                ChooseDirection();
            }
        }
    }

    public void ChooseDirection()
    {
        WalkDirection = Random.Range(0, 4);
        isWalking = true;
        walkCounter = walkTime;
    }
}