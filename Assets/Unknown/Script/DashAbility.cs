using UnityEngine;
using System.Collections;

public class DashAbility : MonoBehaviour
{
    public Transform buildPoint;
    public GameObject buildingBlock;

    public Transform dashPos;
    public float speed;

    private bool isDashing = false;
    public float coolDown = 5.0f;

    public float startTime;
    public string currentTime;
    public float chargeTime = 0;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            StartCoroutine(dashBuild());
        }
    }

    IEnumerator dashBuild()
    {
        isDashing = true;
        Instantiate(buildingBlock, buildPoint.position, buildPoint.rotation);
        transform.Translate(0.5f, 0f, 0f);
        //transform.position = Vector3.Lerp(transform.position, dashPos.position, 20.0f);
        Instantiate(buildingBlock, buildPoint.position, buildPoint.rotation);
        transform.Translate(0.5f, 0f, 0f);
        Instantiate(buildingBlock, buildPoint.position, buildPoint.rotation);
        transform.Translate(0.5f, 0f, 0f);
        Instantiate(buildingBlock, buildPoint.position, buildPoint.rotation);
        yield return new WaitForSeconds(3f);
        isDashing = false;
    }
}