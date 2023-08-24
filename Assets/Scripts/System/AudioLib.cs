using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioLib : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSourceRun;
    [SerializeField] private AudioSource audioSourceClimb;
    [SerializeField] private AudioSource audioSourceCrouch;
    [SerializeField] private AudioSource audioSourceTestFmod;

    public void PlaySound(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void PlayOnce(AudioClip clip)
    {
        audioSource?.PlayOneShot(clip);
    }

    public void PlayOnceRun(AudioClip clip)
    {
        audioSourceRun?.PlayOneShot(clip);
    }

    public void PlayOnceClimb(AudioClip clip)
    {
        audioSourceClimb?.PlayOneShot(clip);
    }

    public void PlayOnceCrouch(AudioClip clip)
    {
        audioSourceCrouch?.PlayOneShot(clip);
    }

    public void StopPlaying()
    {
        audioSource.Stop();
    }

    public void PlayFmodSound()
    {
       FMODUnity.RuntimeManager.PlayOneShot("event:/footsteps");
    }
    
}
