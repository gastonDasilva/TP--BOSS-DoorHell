using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

    public Camera mainCamera;

    float shakeMount = 0;
    // Use this for initialization

    private void Awake()
    {
        if (mainCamera == null) Debug.Log("LA mainCamera ES NULL");
    }


    public void Shake( float amount, float lenght)
    {/* lenght es el tiempo de duracion*/
        shakeMount = amount;
        InvokeRepeating("BeginShake",0,0.01f);
        Invoke("StopShake", lenght);
    }

    void StopShake()
    {
        CancelInvoke("BeginShake");
        mainCamera.transform.localPosition = Vector3.zero;
    }

    

    void BeginShake()
    {
        if(shakeMount > 0)
        {
            Vector3 camPos = mainCamera.transform.position;

            float offsetX = Random.value * shakeMount * 2 - shakeMount;
            float offsetY = Random.value * shakeMount * 2 - shakeMount;

            camPos.x += offsetX;
            camPos.y += offsetY;
            mainCamera.transform.position = camPos;
        }
    }
}
