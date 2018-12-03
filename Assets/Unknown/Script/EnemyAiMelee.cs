using UnityEngine;
using System.Collections;

public class EnemyAiMelee : MonoBehaviour
{
    GameObject player;
    Transform playerTrans;
    Eyes eyes;

    public GameObject throwingStar;

    public float chaseDistance = 10.0f;
    float attackDistance = 2.5f;
    float gettingTooClose = 7.5f;

    int speed;
    bool attacking;

    bool canAttack;
    float attackTimer;

    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player");
        playerTrans = player.transform;
        eyes = GetComponentInChildren<Eyes>();
    }

    // Update is called once per frame
    void Update()
    {
        if (attackTimer <= 0)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
            attackTimer -= Time.deltaTime;
        }
    }

    void FixedUpdate()
    {
        // if the player is within chaseDistance, Move towards him.
        if (Vector2.Distance(transform.position, playerTrans.position) < chaseDistance && !attacking)
        {
            speed = 1;
            GetComponent<Rigidbody2D>().velocity = new Vector2(eyes.playerPos.direction.x * speed, 0);
        }

        if (eyes.playerPos.direction.magnitude < 1)
        {
            eyes.playerPos.direction = eyes.playerPos.direction.normalized;
        }

        // if the player is within attackDistance, attack him/her.
        if (Vector2.Distance(transform.position, playerTrans.position) < attackDistance)
        {
            GetComponent<Rigidbody2D>().isKinematic = true;
            if (canAttack)
            {
                GameObject temp = Instantiate(throwingStar, transform.position, Quaternion.identity) as GameObject;
                //Physics.IgnoreCollision (temp.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
                temp.GetComponent<Rigidbody2D>().AddForce(eyes.transform.forward * 200);
                attacking = true;
                attackTimer += 0.5f;
            }
        }
        else if (Vector2.Distance(transform.position, playerTrans.position) > attackDistance)
        {
            attacking = false;
            GetComponent<Rigidbody2D>().isKinematic = false;
        }

        if (Vector2.Distance(transform.position, playerTrans.position) < gettingTooClose && attacking)
        {
            GetComponent<Rigidbody2D>().isKinematic = false;
            speed = 2;
            GetComponent<Rigidbody2D>().velocity = new Vector2(-eyes.playerPos.direction.x * speed, 0);
        }
    }
}