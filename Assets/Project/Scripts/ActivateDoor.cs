using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDoor : MonoBehaviour, IActivatable
{
    public int id;
    private bool activated;
    private Vector3 initialPos;
    private Vector3 nextPos;

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
        transform.position = nextPos;
    }

    public void Deactivate()
    {
        activated = false;
        transform.position = initialPos;
    }

    public void ToggleActivate()
    {
        activated = !activated;
//        Destroy(gameObject);
        if (activated)
        {
//            transform.Translate(0, -3, 0);
            transform.position = nextPos;
        }
        else
        {
//            transform.Translate(0, 3, 0);
            transform.position = initialPos;
        }
    }

    public int GetId()
    {
        return id;
    }
}