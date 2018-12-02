﻿using System.Linq;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public int targetId;
    private IActivatable target;

    private void Start()
    {
        SearchActivatables();
    }

    private void Update()
    {
    }

    public void SetId(int id)
    {
        this.targetId = id;
    }

    public int GetId()
    {
        return targetId;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (target == null) return;
        if (other.tag.Equals("Player") || other.tag.Equals("Pallet"))
        {
            target.Activate();
        }
    }
    
    public void OnTriggerExit2D(Collider2D other)
    {
        if (target == null) return;
        if (other.tag.Equals("Player") || other.tag.Equals("Pallet"))
        {
            target.Deactivate();
        }
    }

//    public void Activate()
//    {
//        if (target != null)
//        {
//            target.Activate();
//        }
//        else
//        {
//            Debug.LogError("Cant find target id!");
//        }
//    }
//
    private void SearchActivatables()
    {
        var ss = FindObjectsOfType<Object>().OfType<IActivatable>();
        foreach (IActivatable s in ss)
        {
            if (s.GetId() == targetId)
            {
                target = s;
                break;
            }
        }
    }
}