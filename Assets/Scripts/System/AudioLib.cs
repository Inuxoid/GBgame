using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLib : MonoBehaviour
{
    public void PlaySound(AudioClip clip, AudioSource audioSource)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayOnce(AudioClip clip, AudioSource audioSource)
    {
        audioSource?.PlayOneShot(clip);
    }

    public void StopPlaying(AudioSource audioSource)
    {
        audioSource.Stop();
    }
}
