using System.Linq;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public int targetId;
    private IActivatable target;
    private Animator anim;

    private void Start()
    {
        SearchActivatables();
        anim = GetComponent<Animator>();
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
        target.Activate();
    }

    private bool permOpen = false;
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (target == null) return;
        if (permOpen) return;
        if (other.tag.Equals("Player") || other.tag.Equals("Pallet") || other.tag.Equals("PlayerHitbox"))
        {
//            target.Activate();
            anim.Play("Charge");
        }

        if (other.tag.Equals("Pallet"))
        {
            permOpen = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (target == null) return;
        if (permOpen) return;
        if (other.tag.Equals("Player") || other.tag.Equals("Pallet") || other.tag.Equals("PlayerHitbox"))
        {
            target.Deactivate();

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("On") ||
                anim.GetCurrentAnimatorStateInfo(0).IsName("OnLoop"))
            {
                anim.SetTrigger("Uncharge");
            }
            else
            {
                anim.Play("Off");
            }
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