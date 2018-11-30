using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Stuff")]
    public float moveSpeed;
    public float jumpHeight;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public LayerMask whatIsWallUp;

    [Header("Firing Stuff")]
    public GameObject bullet;
    public Transform firePoint;

    [Header("Dashing Stuff")] public Transform dashCheck;
    public float dashCheckRadius;
    public float dashCooldown = 2.0f;

    private Vector2 moveInput;
    private bool toJump = false;
    private bool isGrounded = false;
    private bool canDoubleJump = false;
    private bool isFacingRight = true;
    private bool isDashWallBlocked = false;
    private bool isDashOnCooldown = false;

    private Animator anim;
    private Rigidbody2D rb2d;
    private AudioManager audioManager;

//    public float knockback;
//    public float knockbackCount;
//    public float knockbackLength;
//    public bool knockFromRight;
//    public float maxDash = 10f;
//    public float maxDashUp = 10f;
    
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        audioManager = AudioManager.instance;
        if (audioManager == null) Debug.LogError("No audio Manager found");
    }

    private void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        isDashWallBlocked = Physics2D.OverlapCircle(dashCheck.position, dashCheckRadius, whatIsWall);

        if (isGrounded)
            canDoubleJump = true;

        var newVelocity = rb2d.velocity;
        newVelocity.x = moveInput.x * moveSpeed;
        if (toJump)
        {
            toJump = false;
            newVelocity.y = jumpHeight;
        }

        rb2d.velocity = newVelocity;
    }

    private void Update()
    {
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
            else if (canDoubleJump)
            {
                canDoubleJump = false;
                toJump = true;
                audioManager.PlaySound("Jump");
            }
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            audioManager.PlaySound("Firing");
            SpawnBullet();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!(isDashWallBlocked || isDashOnCooldown))
            {
                audioManager.PlaySound("Powerup");
                dashRight();
                isDashOnCooldown = true;
                StartCoroutine(DoAfterSeconds(dashCooldown, () => isDashOnCooldown = false));
//            anim.SetBool("Blink", true);
            }
        }

//        var moveVelocity = 0f;
//        if (Input.GetKey(KeyCode.D)) moveVelocity = moveSpeed;
//        if (Input.GetKey(KeyCode.A)) moveVelocity = -moveSpeed;
//
//        if (knockbackCount <= 0)
//        {
//            rb2d.velocity = new Vector2(moveVelocity, rb2d.velocity.y);
//        }
//        else
//        {
//            if (knockFromRight)
//                transform.Translate(-0.1f, 0.1f, 0f);
//            if (!knockFromRight)
//                transform.Translate(0.1f, 0.1f, 0f);
//            knockbackCount -= Time.deltaTime;
//        }
    }

    public IEnumerator DoAfterSeconds(float delay, Action op)
    {
        yield return new WaitForSeconds(delay);
        op();
    }

    private void LateUpdate()
    {
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

    private void dashRight()
    {
        var dist = Vector3.right * 2.0f;
        if (!isFacingRight)
            dist *= -1;
        transform.Translate(dist);
    }

    private void SpawnBullet()
    {
        audioManager.PlaySound("Firing");
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}