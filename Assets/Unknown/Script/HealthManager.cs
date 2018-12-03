using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxPlayerHealth;

    public static int playerHealth;

    public bool isDead;

    public float invulnerabilityTimer;

    public bool invulnerable = false;

    public PlayerController player;

    Text text;

    //public Slider healthBar;

    private LevelManager levelManager;

    private LifeManager lifeSystem;

    //cache
    private AudioManager audioManager;

    // Use this for initialization
    void Start()
    {
        text = GetComponent<Text>();
        //healthBar = GetComponent<Slider>();

        playerHealth = maxPlayerHealth;

        levelManager = FindObjectOfType<LevelManager>();

        lifeSystem = FindObjectOfType<LifeManager>();

        player = FindObjectOfType<PlayerController>();

        isDead = false;

        //caching
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No audio Manager found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0 && !isDead)
        {
            playerHealth = 0;
            levelManager.RespawnPlayer();
            lifeSystem.TakeLife();
            isDead = true;
        }

        if (playerHealth > maxPlayerHealth)
            playerHealth = maxPlayerHealth;

        text.text = "" + playerHealth;
        //healthBar.value = playerHealth;

        if (invulnerabilityTimer <= 0)
        {
            invulnerable = false;
        }
        else
        {
            //invulnerable = true;
            Debug.Log("player is invulnerable");
            invulnerabilityTimer -= Time.deltaTime;
        }
    }

    //private IEnumerator IndicateInvulnerable()
    //{
    //while (invulnerable = true) 
    //{
    //player.GetComponent<SpriteRenderer> ().enabled = false;
    //yield return new WaitForSeconds (.1f);
    //player.GetComponent<SpriteRenderer> ().enabled = true;
    //yield return new WaitForSeconds (.1f);
    //}
    //}

    public void HurtPlayer(int damageToGive)
    {
        if (!invulnerable)
        {
            playerHealth -= damageToGive;
            invulnerable = true;
            player.GetComponent<Animation>().Play("Player_Flash_blue");
            audioManager.PlaySound("PlayerHurt");
            //StartCoroutine (IndicateInvulnerable ());
            invulnerabilityTimer += 2.0f;
        }
    }

    public void FullHealth()
    {
        playerHealth = maxPlayerHealth;
    }
}