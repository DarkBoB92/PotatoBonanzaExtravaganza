using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioManager : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip myClip;
    public float volume = 1.0f;

    public void StartAudio()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = myClip;
        audioSource.volume = volume;
        audioSource.Play();
    }

    void Update()
    {
        if (!audioSource.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
