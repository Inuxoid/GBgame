using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dto;
using UnityEngine.Events;

public class Gate : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private Settings settings;
    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private UnityEvent<IntNumberDto> onScoreOutputed;
    [SerializeField] private UnityEvent<IntNumberDto> onTotalScoreOutputed;
    [SerializeField] private UnityEvent<IntNumberDto> onVHSOutputed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            scorePanel.SetActive(true);
            IntNumberDto score = new IntNumberDto() { value = scoreCounter.ScoreCount};
            IntNumberDto scoreTotal = new IntNumberDto() { value = scoreCounter.ScoreCount };
            IntNumberDto VHS = new IntNumberDto() { value = scoreCounter.VHSCount };
            this.onScoreOutputed?.Invoke(score);
            this.onTotalScoreOutputed?.Invoke(scoreTotal);
            this.onVHSOutputed?.Invoke(VHS);
            settings.Pause();
        }
    }
}
