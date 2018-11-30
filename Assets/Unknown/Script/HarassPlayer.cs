using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarassPlayer : MonoBehaviour
{
    public float maxDistance = 1.0f;
    public float moveTime = 1.0f;
    public float waitTime = 1.0f;

    private GameObject player;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player_blue");
        StartCoroutine("RandomMove");
    }

    IEnumerator RandomMove()
    {
        if (player != null)
        {
            float t = 0.0f;
            float currentMoveTime = moveTime;
            Vector3 initialPosition = transform.position;
            Vector3 moveVector = Random.insideUnitCircle * maxDistance;
            Vector3 endPosition = player.transform.position + moveVector;

            if (currentMoveTime < 0.0f)
            {
                currentMoveTime = float.Epsilon;
                Debug.LogWarning("Warning, Warning,current move time is set to 0 or less!");
            }

            while (t < currentMoveTime)
            {
                transform.position = Vector3.Lerp(initialPosition, endPosition, t / currentMoveTime);
                t += Time.deltaTime;
                yield return null;
            }

            yield return new WaitForSeconds(waitTime);
        }
        else
        {
            player = GameObject.FindGameObjectWithTag("Player_blue");
            yield return new WaitForSeconds(waitTime);
        }

        StartCoroutine("RandomMove");
    }
}