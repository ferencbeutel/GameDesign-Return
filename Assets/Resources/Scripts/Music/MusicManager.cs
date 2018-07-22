using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioSource audioSource;
    Tune currentTune;

    public void PlayTune(Tune tune)
    {
        if (currentTune == null || currentTune.name != tune.name)
        {
            currentTune = tune;

            audioSource.Stop();
            audioSource.clip = tune.clip;
            audioSource.Play();
        }
    }

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.loop = true;
    }
}
