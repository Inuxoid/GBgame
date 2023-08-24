using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class VerbalReactions : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI uiText;
    [SerializeField] private GameObject TextCanvas;
    [SerializeField] private string currentText;
    [SerializeField] private int textInd;
    [SerializeField] private int letterInd;
    [SerializeField] private bool isPlaying;
    [SerializeField] private bool isWrote;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private UnityEvent onClick;

    private void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && playerInput.actions["Action"].IsPressed() && !isPlaying)
        {
            this.onClick?.Invoke();
            letterInd = 0;
            StartCoroutine(VerbalTimer());
        }
        else if (other.CompareTag("Player") && playerInput.actions["Action"].IsPressed() && isWrote)
        {
            this.onClick?.Invoke();
            TextCanvas.SetActive(true);
            StartCoroutine(WriteAgain());
        }
    }
    IEnumerator VerbalTimer()
    {
        isPlaying = true;
        while (letterInd < VerbalLib.getInstance().texts[textInd].Length)
        {
            currentText += VerbalLib.getInstance().texts[textInd][letterInd];
            uiText.text = currentText;
            letterInd++;
            yield return new WaitForSeconds(.06f);
        }
        yield return new WaitForSeconds(5f);
        //Destroy(uiText);
        TextCanvas.SetActive(false);
        isWrote = true;
        yield return null;
	}
    IEnumerator WriteAgain()
    {
        yield return new WaitForSeconds(5f);
        TextCanvas.SetActive(false);
    }
}
