using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEffects : MonoBehaviour
{
    public CanvasGroup whiteOverlay;

    float shakeDuration;
    float shakeStrength;
    bool isShaking = false;
    Vector3 originalPosition;

    float flashDuration;
    bool isFlashed = false;

    public void ShakeFor(float seconds, float strength)
    {
        this.shakeDuration = seconds;
        this.shakeStrength = strength;
    }

    public void FlashFor(float seconds)
    {
        this.flashDuration = seconds;
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

        if (flashDuration > 0)
        {
            if (!isFlashed)
            {
                isFlashed = true;
                whiteOverlay.alpha = 1;
            }

            flashDuration -= Time.deltaTime;
        }
        else
        {
            isFlashed = false;
            whiteOverlay.alpha = 0;
        }
    }
}
