using UnityEngine;
using System.Collections;

public class ScreenShake : MonoBehaviour {
       
    float shakeAmmount;

    public Camera mainCamera;


    public void CallScreenShake(float shakeDuration = 0.5f, float shakeValue = 0.2f)
    {
        shakeAmmount = shakeValue;
        InvokeRepeating("ShakeScreen", 0f, 0.05f);
        Invoke("StopShaking", shakeDuration);
    }

    void ShakeScreen()
    {
        if (shakeAmmount > 0)
        {
            float quakeAmt = Random.value * shakeAmmount * 2 - shakeAmmount;
            Vector3 pp = mainCamera.transform.position;
            pp.y += quakeAmt;
            pp.x += quakeAmt;
            mainCamera.transform.position = pp;
        }
    }

    void StopShaking()
    {
        CancelInvoke("ShakeScreen");
    }
}
