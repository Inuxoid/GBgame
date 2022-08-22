using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logo : MonoBehaviour
{
    [SerializeField] private GameObject logoPic;
    [SerializeField] private GameObject logoPic2;

    void Start()
    {
        StartCoroutine(ShowLogo());
    }

    IEnumerator ShowLogo()
    {
        logoPic.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        logoPic.SetActive(false);
        logoPic2.SetActive(true);
        yield return new WaitForSeconds(2);
        logoPic2.SetActive(false);
    }
}
