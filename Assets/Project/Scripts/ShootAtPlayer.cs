using UnityEngine;
using System.Collections;

public class ShootAtPlayer : MonoBehaviour
{
    public GameObject projectile;
    public float speedFactor;
    public float Delay;

    private Transform launchPoint;

    private void Start()
    {
        StartCoroutine(Shoots());
        launchPoint = transform.Find("launchPoint");
    }


    private IEnumerator Shoots()
    {
        while (true)
        {
            yield return new WaitForSeconds(Delay);
            var clone = Instantiate(projectile, launchPoint.position, Quaternion.identity);
            var vel = -new Vector2(transform.localScale.x, 0f) * speedFactor;
            clone.GetComponent<Rigidbody2D>().velocity = vel;
        }
    }
}