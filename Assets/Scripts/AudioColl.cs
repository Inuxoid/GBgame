using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioColl : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private List<float> audioVolumes;
    [SerializeField] private bool cached;
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
            item.volume -= item.volume / 4;
            yield return new WaitForSeconds(0.6f);
        }
        item.volume = 0;
        yield return null;
    }

    IEnumerator soundUnTimer(AudioSource item, int newI)
    {
        while (item.volume < audioVolumes[newI] / 2)
        {
            item.volume += 0.1f + item.volume * 2;
            yield return new WaitForSeconds(0.6f);
        }
        item.volume = audioVolumes[newI];
        yield return null;
    }
}
