using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VerbalReactions : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI uiText;
    [SerializeField] private string currentText;
    [SerializeField] private int textInd;
    [SerializeField] private int letterInd;
    [SerializeField] private bool isPlaying;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isPlaying)
        {
            letterInd = 0;
            StartCoroutine(VerbalTimer());
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
        Destroy(uiText);
		yield return null;
	}
}
