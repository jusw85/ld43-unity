using System.Linq;
using UnityEngine;

public class RemoteContactSwitch : MonoBehaviour, IActivator
{
    public int targetId;
    private IActivatable target;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

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

    private bool activated = false;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (target == null) return;
        if (activated) return;
        if (other.tag.Equals("Player") || other.tag.Equals("Pallet") || other.tag.Equals("PlayerHitbox"))
        {
            target.Activate();
            anim.SetTrigger("activate");
            activated = true;
//            anim.Play("Charge");
        }

//        if (other.tag.Equals("Pallet"))
//        {
//            permOpen = true;
//        }
    }

    public void Activate()
    {
//        anim.SetTrigger("activate");
//        if (target != null)
//        {
//            target.ToggleActivate();
//        }
//        else
//        {
//            Debug.LogError("Cant find target id!");
//        }
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