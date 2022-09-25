using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class ActivateShopButton : MonoBehaviour
{
    [SerializeField] private bool isOpening;
    [SerializeField] private bool isOpened;
    [SerializeField] private bool isTrying;
    [SerializeField] private UnityEvent onPushed;
    [SerializeField] private UnityEvent unPushed;

    private void ColorChange()
    {
        if (isOpened)
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isTrying = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            isTrying = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isTrying && !isOpening)
        {
            StartCoroutine(ButtonTimer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isTrying && !isOpening && isOpened)
        {
            StartCoroutine(ButtonTimer());
        }
    }

    IEnumerator ButtonTimer()
    {
        ColorChange();
        isOpening = true;
        isOpened = !isOpened;
        if (isOpened)
        {
            onPushed?.Invoke();
            yield return new WaitForSeconds(0.4f);
        }
        else
        {
            unPushed?.Invoke();
            yield return new WaitForSeconds(0.4f);
        }
        isOpening = false;
        yield return null;
    }
}
