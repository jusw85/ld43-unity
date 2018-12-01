using UnityEngine;
using System.Collections;
using Tiled2Unity;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed;
    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    public Transform edgeCheck;
    public bool initialFacingRight = true;

    private Rigidbody2D rb2d;
    private Transform groundCheck;
    private bool facingRight;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("groundCheck");
        facingRight = initialFacingRight;
    }

    private void FixedUpdate()
    {
        var grounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, whatIsWall);
        if (grounded)
        {
//            Debug.Log("Is Grounded");
            var hittingWall = Physics2D.OverlapCircle(wallCheck.position, 0.05f, whatIsWall);
            var isAtEdge = !Physics2D.OverlapCircle(edgeCheck.position, wallCheckRadius, whatIsWall);

            if (hittingWall || isAtEdge)
//            if (hittingWall)
//            if (isAtEdge)
            {
//                Debug.Log("Do Flip");
                facingRight = !facingRight;
            }
        }

        if (facingRight)
        {
            transform.localScale = new Vector3(1f, 1f, 0f);
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
        }

        if (!facingRight)
        {
            transform.localScale = new Vector3(-1f, 1f, 0f);
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
        }
    }

//    void Flip()
//    {
//        facingRight = !facingRight;
//        Vector3 theScale = transform.localScale;
//        theScale.x *= -1;
//        transform.localScale = theScale;
//    }
}