using UnityEngine;
using System.Collections;

public class FmodSoundScript : MonoBehaviour
{
    public void PlayFmodSound(AudioClip clip)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/test");
    }
}