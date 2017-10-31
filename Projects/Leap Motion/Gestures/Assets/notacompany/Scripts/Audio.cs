using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static void Play(AudioSource audioSource, AudioClip audioClip)
    {
        if (!audioSource.isPlaying)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }

    public static void Pause(AudioSource audioSource)
    {
        if (audioSource.isPlaying)
            audioSource.Pause();
    }

    public static void Stop(AudioSource audioSource)
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}