using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDoor : MonoBehaviour, IActivatable
{
    public int id;

    private void Start()
    {
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
        Destroy(gameObject);
    }

    public int GetId()
    {
        return id;
    }
}