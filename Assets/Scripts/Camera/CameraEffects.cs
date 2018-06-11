using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    float shakeDuration;
    float shakeStrength;
    bool isShaking = false;
    Vector3 originalPosition;

    public void ShakeFor(float seconds, float strength)
    {
        this.shakeDuration = seconds;
        this.shakeStrength = strength;
    }

    void OnEnable()
    {
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            if (!isShaking)
            {
                // beginning of a new shaking event
                originalPosition = transform.localPosition;
                isShaking = true;
            }

            transform.localPosition = originalPosition + Random.insideUnitSphere * shakeStrength;

            shakeDuration -= Time.deltaTime;
        }
        else
        {
            // not shaking anymore
            isShaking = false;
        }
    }
}
