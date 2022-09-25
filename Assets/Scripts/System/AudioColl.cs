using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioColl : MonoBehaviour
{
    [SerializeField] private bool muteOnStart;
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private List<float> audioVolumes;
    [SerializeField] private bool cached;
    [SerializeField] private bool isInc;

    private void Start()
    {
        if (muteOnStart)
        {
            foreach (var item in audioSources)
            {
                audioVolumes.Add(item.volume);
            }
            cached = true;

            foreach (var item in audioSources)
            {
                StartCoroutine(soundTimer(item));
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {    
            foreach (var item in audioSources)
            {
                StartCoroutine(soundTimer(item));
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            int i = 0;
            foreach (var item in audioSources)
            {
                if (!cached)
                    audioVolumes.Add(item.volume);
                StartCoroutine(soundUnTimer(item, i));
                i++;
                item.Play();
            }
            cached = true;
        }
    }

    IEnumerator soundTimer(AudioSource item)
    {   
        for (int i = 0; i < 5; i++)
        {
            if (!isInc)
                item.volume -= item.volume / 2;
            else
                yield return null;
            yield return new WaitForSeconds(0.2f);
        }
        if (!isInc)
            item.volume = 0;
        yield return null;
    }

    IEnumerator soundUnTimer(AudioSource item, int newI)
    {
        isInc = true;
        while (item.volume < audioVolumes[newI] / 2)
        {
            item.volume += 0.1f + item.volume * 2;
            yield return new WaitForSeconds(0.2f);
        }
        item.volume = audioVolumes[newI];
        isInc = false;
        yield return null;
    }
}
