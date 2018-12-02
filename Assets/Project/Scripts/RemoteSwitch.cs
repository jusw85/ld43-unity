using System.Linq;
using UnityEngine;

public class RemoteSwitch : MonoBehaviour, IActivator
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

    public void Activate()
    {
        if (target != null)
        {
            target.Activate();
        }
        else
        {
            Debug.LogError("Cant find target id!");
        }
    }

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