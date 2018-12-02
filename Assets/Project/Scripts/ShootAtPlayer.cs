using UnityEngine;
using System.Collections;

public class ShootAtPlayer : MonoBehaviour
{
    public GameObject projectile;
    public float speedFactor;
    public float initialDelay = 2.5f;
    public float Delay;

    [System.NonSerialized]
    public bool isShooting = false;

    private Transform launchPoint;

    private void Start()
    {
        launchPoint = transform.Find("launchPoint");
        StartCoroutine(Shoots());
    }

    private IEnumerator Shoots()
    {
        yield return new WaitForSeconds(initialDelay);
        while (true)
        {
            yield return new WaitForSeconds(Delay);
            if (isShooting)
            {
                var clone = Instantiate(projectile, launchPoint.position, Quaternion.identity);
                var vel = new Vector2(transform.localScale.x, 0f) * speedFactor;
                clone.GetComponent<Rigidbody2D>().velocity = vel;
            }
        }
    }
}