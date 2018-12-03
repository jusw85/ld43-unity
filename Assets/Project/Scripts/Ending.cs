using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public AudioClip ac;
    private void Start()
    {
        Camera.main.transform.position = new Vector3(0, 0, -10f);
        AudioManager.instance.PlayBGM(ac);
    }

    private void Update()
    {
    }
}