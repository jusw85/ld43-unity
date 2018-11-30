using UnityEngine;
using System.Collections;

public class NPC_Follow : MonoBehaviour
{
    private PlayerController thePlayer;

    public float moveSpeed;

    public float playerRange;

    public LayerMask playerLayer;

    public bool playerInRange;

    // Use this for initialization
    void Start()
    {
        thePlayer = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInRange = Physics2D.OverlapCircle(transform.position, playerRange, playerLayer);

        if (playerInRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position,
                moveSpeed * Time.deltaTime);
        }

        if (thePlayer.transform.localScale.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

        else
            GetComponent<SpriteRenderer>().flipX = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, playerRange);
    }
}