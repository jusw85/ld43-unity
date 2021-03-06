using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stuff")]
    public float moveSpeed;

    public float jumpHeight;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public GameObject xylemStick;

    public bool enableXylemPhysics;
    public float xylemVelocityMultiplier = 5.0f;

    private Vector2 moveInput;
    private bool toJump;
    private bool isGrounded;
    private bool isFacingRight = true;

    private Animator anim;
    private Rigidbody2D rb2d;
    private AudioManager audioManager;
    private HashSet<IActivator> activators;

    [System.NonSerialized]
    public SpawnPointController spawnPoint;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        activators = new HashSet<IActivator>();
    }

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null) Debug.LogError("No audio Manager found");
//        Camera.main.GetComponent<Camera2DFollow>().target = transform;
    }

    private void FixedUpdate()
    {
        if (isSpawning || isDying) return;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        var newVelocity = rb2d.velocity;
        newVelocity.x = moveInput.x * moveSpeed;
        if (toJump)
        {
            toJump = false;
            newVelocity.y = jumpHeight;
        }

        rb2d.velocity = newVelocity;
    }

    private bool isSpawning = true;
    private bool isDying = false;

    public bool IsDying
    {
        get { return isDying; }
        set { isDying = value; }
    }

    public bool IsSpawning
    {
        get { return isSpawning; }
        set { isSpawning = value; }
    }

    private void Update()
    {
        if (isSpawning || isDying) return;

        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveInput.x != 0)
            SetIsFacingRight(moveInput.x > 0);

        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                toJump = true;
                audioManager.PlaySound("Jump");
            }
        }

        if (Input.GetButtonDown("Fire"))
        {
            foreach (IActivator ia in activators)
            {
                ia.Activate();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D c2d)
    {
        IActivator ia = c2d.GetComponent<IActivator>();
        if (ia != null)
        {
            activators.Add(ia);
        }
    }

    private void OnTriggerExit2D(Collider2D c2d)
    {
        IActivator ia = c2d.GetComponent<IActivator>();
        if (ia != null)
        {
            
            activators.Remove(ia);
        }
    }

    private Vector2 oldVel;

    public void Death()
    {
        if (!isDying)
        {
            isDying = true;
            anim.Play("Death");
            oldVel = rb2d.velocity;
            Destroy(rb2d);
            GetComponentInChildren<CircleCollider2D>().enabled = false;
            GetComponentInChildren<BoxCollider2D>().enabled = false;
        }
    }

    public GameObject splatter;

    public void PostDeathAnimation()
    {
        if (spawnPoint != null)
        {
            spawnPoint.Spawn();
        }

        if (splatter != null)
        {
            var obj = Instantiate(splatter, transform.position, Quaternion.identity);
            obj.transform.localScale = transform.localScale;
        }

        if (xylemStick != null)
        {
            var xylem = Instantiate(xylemStick, transform.position, Quaternion.identity);
            if (enableXylemPhysics)
            {
                var vel = new Vector2(oldVel.x, -oldVel.y);
                vel.Normalize();
                vel *= xylemVelocityMultiplier;
                xylem.GetComponent<Rigidbody2D>().velocity = vel;
            }
            else
            {
                xylem.GetComponent<Rigidbody2D>().constraints =
                    RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
            }
        }

        Destroy(gameObject);
    }

    public IEnumerator DoAfterSeconds(float delay, Action op)
    {
        yield return new WaitForSeconds(delay);
        op();
    }

    private void LateUpdate()
    {
        if (isSpawning || isDying) return;
        anim.SetBool("Ground", isGrounded);
        anim.SetFloat("vSpeed", rb2d.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(moveInput.x));
    }

    private void SetIsFacingRight(bool isFacingRight)
    {
        if (this.isFacingRight ^ isFacingRight)
        {
            var scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }

        this.isFacingRight = isFacingRight;
    }
}