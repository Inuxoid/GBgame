using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FInalCutscene : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject slide;
    [SerializeField] private Settings settings;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            animator.SetBool("Play", true);
            StartCoroutine(EndTimer());
        }
        
    }

    IEnumerator EndTimer()
    {
        yield return new WaitForSeconds(10f);
        slide.SetActive(true);
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
        yield return null;
    }
}
