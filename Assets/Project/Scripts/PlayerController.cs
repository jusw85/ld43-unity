using System;
using System.Collections;
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
    public LayerMask whatIsWall;
//    public LayerMask whatIsWallUp;
    public float maxLifeTime;
    public Slider lifeTimeSlider;

    [Header("Firing Stuff")]
    public GameObject bullet;

    public Transform firePoint;
    public float fireCooldown = 0.5f;

    [Header("Dashing Stuff")]
    public Transform dashCheck;

    public float dashCheckRadius;
    public float dashCooldown = 2.0f;

    private Vector2 moveInput;
    private bool toJump;
    private bool isGrounded;
    private bool canDoubleJump;
    private bool isFacingRight = true;
    private bool isFireOnCooldown;
    private bool isDashWallBlocked;
    private bool isDashOnCooldown;
    private float currentLifeTime;

    private Animator anim;
    private Rigidbody2D rb2d;
    private AudioManager audioManager;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        currentLifeTime = maxLifeTime;
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
        currentLifeTime -= Time.deltaTime;
        lifeTimeSlider.value = currentLifeTime / maxLifeTime;
        if (currentLifeTime <= 0f)
        {
            KillMyself();
        }
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

        if (Input.GetButton("Fire"))
        {
            if (!isFireOnCooldown)
            {
                isFireOnCooldown = true;
                FireBullet();
                StartCoroutine(DoAfterSeconds(fireCooldown, () => isFireOnCooldown = false));
            }
        }

        if (Input.GetButtonDown("Blink"))
        {
            if (!(isDashWallBlocked || isDashOnCooldown))
            {
                isDashOnCooldown = true;
                DashRight();
                StartCoroutine(DoAfterSeconds(dashCooldown, () => isDashOnCooldown = false));
                anim.SetTrigger("Blink");
            }
        }
    }

    private void KillMyself()
    {
        Destroy(gameObject);
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

    private void DashRight()
    {
        var dist = Vector3.right * 2.0f;
        if (!isFacingRight)
            dist *= -1;
        transform.Translate(dist);
        audioManager.PlaySound("Powerup");
    }

    private void FireBullet()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation);
        audioManager.PlaySound("Firing");
    }
}