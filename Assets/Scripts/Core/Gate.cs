using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private Settings settings;
    private void OnTriggerEnter(Collider other)
    {
        scorePanel.SetActive(true);
        settings.Pause();
    }
}
