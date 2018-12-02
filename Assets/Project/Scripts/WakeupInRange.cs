using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeupInRange : MonoBehaviour
{
    private ShootAtPlayer shoot;
    private Animator anim;
    private Light light;

    private void Start()
    {
        shoot = transform.parent.GetComponent<ShootAtPlayer>();
        anim = transform.parent.GetComponent<Animator>();
        light = transform.parent.GetComponentInChildren<Light>();
    }

    private void Update()
    {
    }

    private Coroutine wakeup;
    private Coroutine gosleep;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            StopAllCoroutines();
            wakeup = StartCoroutine(Wakeup());
        }
    }

    private IEnumerator Wakeup()
    {
        light.enabled = true;
        yield return new WaitForSeconds(0.1f);
        shoot.isShooting = true;
        anim.Play("Wake");
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            StopAllCoroutines();
            gosleep = StartCoroutine(GoSleep());
        }
    }

    private IEnumerator GoSleep()
    {
        shoot.isShooting = false;
        anim.Play("Sleep");
        yield return new WaitForSeconds(0.8f);
        light.enabled = false;
    }
}