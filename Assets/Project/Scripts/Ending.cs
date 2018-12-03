using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public AudioClip ac;
    private void Start()
    {
        Camera.main.transform.position = Vector3.zero;
        AudioManager.instance.PlayBGM(ac);
    }

    private void Update()
    {
    }
}