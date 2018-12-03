using UnityEngine;
using System.Collections;

public class ShootAtPlayerInRange1 : MonoBehaviour
{
    public float playerRange;

    public GameObject enemyBullet;

    public PlayerController player;

    public Transform launchPoint;

    public Transform launchPoint2;

    public float waitBetweenShots;

    private float shotCounter;

    // Use this for initialization
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

        shotCounter = waitBetweenShots;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(new Vector3(transform.position.x - playerRange, transform.position.y, transform.position.z),
            new Vector3(transform.position.x + playerRange, transform.position.y, transform.position.z));
        shotCounter -= Time.deltaTime;

        if (transform.localScale.x > 0 && player.transform.position.x > transform.position.x &&
            player.transform.position.x < transform.position.x + playerRange && shotCounter < 0)
        {
            Instantiate(enemyBullet, launchPoint.position, launchPoint.rotation);
            shotCounter = waitBetweenShots;
        }

        if (transform.localScale.x < 0 && player.transform.position.x < transform.position.x &&
            player.transform.position.x > transform.position.x - playerRange && shotCounter < 0)
        {
            Instantiate(enemyBullet, launchPoint2.position, launchPoint2.rotation);
            shotCounter = waitBetweenShots;
        }
    }
}