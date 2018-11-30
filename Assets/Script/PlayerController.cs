using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;

    public float jumpHeight;
    bool facingRight = true;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    private bool grounded;
    private bool canDoubleJump;

    public Transform buildPoint;
    public GameObject buildingBlock;

    public Transform telePoint;
    public GameObject teleBlock;

    public float buildDelay;
    private float buildDelayCounter;

    public float boundDelay;
    private float boundDelayCounter;

	public Transform firePoint;
	public GameObject bullet;

	public Transform firePoint_red;
	public GameObject bullet_red;

    public float knockback;
    public float knockbackLength;
    public float knockbackCount;
    public bool knockFromRight;

    public DashState dashState;
    public float dashTimer;
    public float maxDash = 10f;

    public DashUpState dashUpState;
    public float dashUpTimer;
    public float maxDashUp = 10f;

    public Transform dashCheck;
    public float dashCheckRadius;
    public LayerMask whatIsWall;
    private bool cannotDash;

    public Transform dashUpCheck;
    public float dashUpCheckRadius;
    public LayerMask whatIsWallUp;
    private bool cannotDashUp;

	public Transform dashBulletPoint;
	public GameObject dashBullet;

   public Transform boundPoint;
    public GameObject phantomBound;
   //private GameObject phantomBoundInstance;
   //private bool isPhantomBoundActive = false;

	public Sprite PlayerRed;
	public Sprite PlayerBlue;

	private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb2d;
    private Animator anim;

    private float currentScale = 1.0f;
    private Vector2 moveInput;
    private bool isFacingRight = true;
    private bool toJump = false;



	//cache
	private AudioManager audioManager;

    // Use this for initialization
    private void Awake() 
	{
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }

	void Start()
	{
		//caching
		audioManager = AudioManager.instance;
		if (audioManager == null) 
		{
			Debug.LogError ("No audio Manager found");
		}

		spriteRenderer = GetComponent<SpriteRenderer> ();

		if (spriteRenderer.sprite == null)
			spriteRenderer.sprite = PlayerBlue;
			
	}

    private void FixedUpdate() {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
        cannotDash = Physics2D.OverlapCircle(dashCheck.position, dashCheckRadius, whatIsWall);
        cannotDashUp = Physics2D.OverlapCircle(dashUpCheck.position, dashUpCheckRadius, whatIsWallUp);

        if (grounded)
            canDoubleJump = true;

        Vector2 newVelocity = rb2d.velocity;
        newVelocity.x = moveInput.x * moveSpeed;
        if (toJump) {
            toJump = false;
            newVelocity.y = jumpHeight;
        }
        rb2d.velocity = newVelocity;
    }

    // Update is called once per frame
    void Update() 
	{
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (moveInput.x != 0)
            SetIsFacingRight(moveInput.x > 0);

        if (Input.GetKeyDown(KeyCode.E)) {
            currentScale = 1.5f - currentScale;
            RescaleSprite(currentScale);
        }

        if (Input.GetButtonDown("Jump")) 
		{
            if (grounded) 
			{
                toJump = true;
				audioManager.PlaySound ("Jump");
            } else if (canDoubleJump) {
                canDoubleJump = false;
                toJump = true;
				audioManager.PlaySound ("Jump");
            }
        }

		if (Input.GetKeyDown (KeyCode.L)) 
		{
			audioManager.PlaySound ("Firing");
			PlayerColorBullet ();
			anim.SetBool ("Shooting", true);
		} 
		else 
		{
			anim.SetBool ("Shooting", false);
		}
	



        //This is to create phantom bound
        if (Input.GetKeyDown(KeyCode.K) && Time.time > boundDelayCounter) 
		{
            boundDelayCounter = Time.time + boundDelay;
            Instantiate(phantomBound, boundPoint.position, boundPoint.rotation);
        }



        float moveVelocity = 0f;
        if (Input.GetKey(KeyCode.D)) {
            //GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);

			moveVelocity = moveSpeed;
        }
        if (Input.GetKey(KeyCode.A)) {
            //GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);

			moveVelocity = -moveSpeed;
        }
			

        if (knockbackCount <= 0) 
		{
            rb2d.velocity = new Vector2(moveVelocity, rb2d.velocity.y);
        } 
		else 
		 {
            if (knockFromRight)
                transform.Translate(-0.1f, 0.1f, 0f);
            if (!knockFromRight)
                transform.Translate(0.1f, 0.1f, 0f);
            knockbackCount -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Return)) {
            Instantiate(buildingBlock, buildPoint.position, buildPoint.rotation);
            buildDelayCounter = buildDelay;
        }

        if (Input.GetKey(KeyCode.Return)) {
            buildDelayCounter -= Time.deltaTime;

            if (buildDelayCounter <= 0) {
                buildDelayCounter = buildDelay;
                Instantiate(buildingBlock, buildPoint.position, buildPoint.rotation);
            }
        }

        //This is to teleport the character to the right
        switch (dashState) 
		{
		case DashState.Ready: 
			{
				if (Input.GetKeyDown (KeyCode.P) && !cannotDash) 
				{
					audioManager.PlaySound ("Powerup"); 
					PlayerColorBullet ();
					dashRight ();
					dashState = DashState.Dashing;
					anim.SetBool ("Blink", true);
				} 
				else 
				{
					anim.SetBool ("Blink", false);
				}
			}
                break;

            case DashState.Dashing:
                dashTimer += Time.deltaTime * 3;
                if (dashTimer >= maxDash) {
                    dashTimer = maxDash;
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0) {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
        }

        //This is to teleport the character to the top
        switch (dashUpState) {
            case DashUpState.Ready: {
                    if (Input.GetKeyDown(KeyCode.I) && !cannotDashUp) 
				{
					dashUp();
                    dashUpState = DashUpState.Dashing;
                    }
                }
                break;
                
            case DashUpState.Dashing:
                dashUpTimer += Time.deltaTime * 3;
                if (dashUpTimer >= maxDashUp) {
                    dashUpTimer = maxDashUp;
                    dashUpState = DashUpState.Cooldown;
                }
                break;

            case DashUpState.Cooldown:
                dashUpTimer -= Time.deltaTime;
                if (dashUpTimer <= 0) {
                    dashUpTimer = 0;
                    dashUpState = DashUpState.Ready;
                }
                break;
        }

		//this is to change Player from Red to blue to Red

		if (Input.GetKeyDown(KeyCode.U)) 
		{
			audioManager.PlaySound ("Powerup");
			PlayerColorChange ();
		}
    }

    private void LateUpdate() {
        anim.SetBool("Ground", grounded);
        anim.SetFloat("vSpeed", rb2d.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(moveInput.x));
    }

    private void SetIsFacingRight(bool isFacingRight) {
        if (this.isFacingRight ^ isFacingRight) {
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
        this.isFacingRight = isFacingRight;
    }

    private void RescaleSprite(float newscale) {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(scale.x) * newscale;
        scale.y = newscale;
        transform.localScale = scale;
    }

    public enum DashState {
        Ready,
        Dashing,
        Cooldown
    }

    public enum DashUpState {
        Ready,
        Dashing,
        Cooldown
    }

    private void dashRight()
	{
		for (int i = 0; i < 5; i++) 
		{
			if (isFacingRight) 
			{
				//GetComponent<Rigidbody2D> ().velocity = new Vector3 (2, 0, 0);
				transform.Translate (0.4f, 0f, 0f);
				Instantiate(buildingBlock, buildPoint.position, buildPoint.rotation);
				Instantiate(dashBullet, dashBulletPoint.position, dashBulletPoint.rotation);
			} else 
			{
				//GetComponent<Rigidbody2D> ().velocity = new Vector3 (-2, 0, 0);
				transform.Translate (-0.4f, 0f, 0f);
				Instantiate(buildingBlock, buildPoint.position, buildPoint.rotation);
				Instantiate(dashBullet, dashBulletPoint.position, dashBulletPoint.rotation);
			}
		}
	}



    private void dashUp() 
	{
        for (int i = 0; i < 5; i++) 
		{
            transform.Translate(0f, 0.3f, 0f);
            //Instantiate(teleBlock, telePoint.position, telePoint.rotation);
        }
    }

	void PlayerColorChange ()
	{
		if (spriteRenderer.sprite == PlayerBlue) 
		{
			spriteRenderer.sprite = PlayerRed;
			gameObject.tag = "Player_red";
		} 
		else 
		{
			spriteRenderer.sprite = PlayerBlue;
			gameObject.tag = "Player_blue";
		}
	}


	void PlayerColorBullet()
	{
		if (gameObject.tag == "Player_blue") 
		{
			//audioManager.PlaySound ("Firing");
			Instantiate (bullet, firePoint.position, firePoint.rotation);
		} 
		else 
		{
			//audioManager.PlaySound ("Firing");
			Instantiate (bullet_red, firePoint.position, firePoint.rotation);
		}
	}

}
