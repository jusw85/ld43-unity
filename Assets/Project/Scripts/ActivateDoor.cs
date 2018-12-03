using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDoor : MonoBehaviour, IActivatable
{
    public int id;
    private bool activated;
    private Vector3 initialPos;
    private Vector3 nextPos;
    private Collider2D c2d;
    private Animator anim;

    private void Awake()
    {
        c2d = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        initialPos = transform.position;
        nextPos = transform.position + new Vector3(0, 3, 0);
    }

    private void Update()
    {
    }

    public void SetId(int id)
    {
        this.id = id;
    }

    public void Activate()
    {
        activated = true;
//        transform.position = nextPos;
        c2d.enabled = false;
        anim.Play("Opening", -1, 0f);
    }

    public void Deactivate()
    {
        activated = false;
//        transform.position = initialPos;
        c2d.enabled = true;
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Opening") ||
            anim.GetCurrentAnimatorStateInfo(0).IsName("Opened"))
        {
            anim.Play("Closing");
        }
    }

    public void ToggleActivate()
    {
        activated = !activated;
//        Destroy(gameObject);
        if (activated)
        {
//            transform.Translate(0, -3, 0);
//            transform.position = nextPos;
            Activate();
        }
        else
        {
//            transform.Translate(0, 3, 0);
//            transform.position = initialPos;
            Deactivate();
        }
    }

    public int GetId()
    {
        return id;
    }
}