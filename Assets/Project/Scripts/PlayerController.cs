using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }

    public enum DashUpState
    {
        Ready,
        Dashing,
        Cooldown
    }

    private Animator anim;
    private AudioManager audioManager;

    public GameObject buildingBlock;
    public Transform buildPoint;
    public GameObject bullet;
    private bool canDoubleJump;
    private bool cannotDash;
    private bool cannotDashUp;
    private bool facingRight = true;

    [Space(10)] [Header("Dashing Stuff")] public GameObject dashBullet;
    public Transform dashBulletPoint;
    public Transform dashCheck;
    public float dashCheckRadius;
    public DashState dashState;
    public float dashTimer;
    public Transform dashUpCheck;
    public float dashUpCheckRadius;
    public DashUpState dashUpState;
    public float dashUpTimer;
    [Space(20)] public Transform firePoint;
    public Transform groundCheck;
    public float groundCheckRadius;

    private bool grounded;
    private bool isFacingRight = true;
    public float jumpHeight;

    public float knockback;
    public float knockbackCount;
    public float knockbackLength;
    public bool knockFromRight;
    public float maxDash = 10f;
    public float maxDashUp = 10f;
    private Vector2 moveInput;
    public float moveSpeed;

    public Sprite PlayerBlue;

    private Rigidbody2D rb2d;

    private SpriteRenderer spriteRenderer;

//    public GameObject teleBlock;
//    public Transform telePoint;

    private bool toJump;
    public LayerMask whatIsGround;
    public LayerMask whatIsWall;
    public LayerMask whatIsWallUp;

    // Use this for initialization
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        //caching
        audioManager = AudioManager.instance;
        if (audioManager == null) Debug.LogError("No audio Manager found");

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer.sprite == null)
            spriteRenderer.sprite = PlayerBlue;
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        cannotDash = Physics2D.OverlapCircle(dashCheck.position, dashCheckRadius, whatIsWall);
        cannotDashUp = Physics2D.OverlapCircle(dashUpCheck.position, dashUpCheckRadius, whatIsWallUp);

        if (grounded)
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

    // Update is called once per frame
    private void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveInput.x != 0)
            SetIsFacingRight(moveInput.x > 0);

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
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
            PlayerColorBullet();
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

        //This is to teleport the character to the right
        switch (dashState)
        {
            case DashState.Ready:
            {
                if (Input.GetKeyDown(KeyCode.P) && !cannotDash)
                {
                    audioManager.PlaySound("Powerup");
                    PlayerColorBullet();
                    dashRight();
                    dashState = DashState.Dashing;
                    anim.SetBool("Blink", true);
                }
                else
                {
                    anim.SetBool("Blink", false);
                }
            }
                break;

            case DashState.Dashing:
                dashTimer += Time.deltaTime * 3;
                if (dashTimer >= maxDash)
                {
                    dashTimer = maxDash;
                    dashState = DashState.Cooldown;
                }

                break;
            case DashState.Cooldown:
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }

                break;
        }

        //This is to teleport the character to the top
        switch (dashUpState)
        {
            case DashUpState.Ready:
            {
                if (Input.GetKeyDown(KeyCode.I) && !cannotDashUp)
                {
                    dashUp();
                    dashUpState = DashUpState.Dashing;
                }
            }
                break;

            case DashUpState.Dashing:
                dashUpTimer += Time.deltaTime * 3;
                if (dashUpTimer >= maxDashUp)
                {
                    dashUpTimer = maxDashUp;
                    dashUpState = DashUpState.Cooldown;
                }

                break;

            case DashUpState.Cooldown:
                dashUpTimer -= Time.deltaTime;
                if (dashUpTimer <= 0)
                {
                    dashUpTimer = 0;
                    dashUpState = DashUpState.Ready;
                }

                break;
        }
    }

    private void LateUpdate()
    {
        anim.SetBool("Ground", grounded);
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
        for (var i = 0; i < 5; i++)
            if (isFacingRight)
            {
                //GetComponent<Rigidbody2D> ().velocity = new Vector3 (2, 0, 0);
                transform.Translate(0.4f, 0f, 0f);
                Instantiate(buildingBlock, buildPoint.position, buildPoint.rotation);
                Instantiate(dashBullet, dashBulletPoint.position, dashBulletPoint.rotation);
            }
            else
            {
                //GetComponent<Rigidbody2D> ().velocity = new Vector3 (-2, 0, 0);
                transform.Translate(-0.4f, 0f, 0f);
                Instantiate(buildingBlock, buildPoint.position, buildPoint.rotation);
                Instantiate(dashBullet, dashBulletPoint.position, dashBulletPoint.rotation);
            }
    }


    private void dashUp()
    {
        for (var i = 0; i < 5; i++)
            transform.Translate(0f, 0.3f, 0f);
        //Instantiate(teleBlock, telePoint.position, telePoint.rotation);
    }

    private void PlayerColorBullet()
    {
        audioManager.PlaySound("Firing");
        Instantiate(bullet, firePoint.position, firePoint.rotation);
    }
}