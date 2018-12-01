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
    public float maxLifeTime;
    public Slider lifeTimeSlider;

    private Vector2 moveInput;
    private bool toJump;
    private bool isGrounded;
    private bool isFacingRight = true;
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
        }
    }

    public void KillMyself()
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

}