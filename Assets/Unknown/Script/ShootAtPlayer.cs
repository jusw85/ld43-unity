using UnityEngine;
using System.Collections;

public class ShootAtPlayer : MonoBehaviour
{
    public GameObject projectile;
    public float speedFactor;
    public float Delay;

    private void Start()
    {
        StartCoroutine(Shoots());
    }


    private IEnumerator Shoots()
    {
        while (true)
        {
            yield return new WaitForSeconds(Delay);
            var clone = Instantiate(projectile, transform.position, Quaternion.identity);
            clone.GetComponent<Rigidbody2D>().velocity = transform.right * speedFactor;
        }
    }
}