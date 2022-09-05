using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioColl : MonoBehaviour
{
    [SerializeField] private AudioSource[] audioSources;
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
}
