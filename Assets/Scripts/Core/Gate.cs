using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dto;
using UnityEngine.Events;
using Newtonsoft.Json;
using System.IO;
using UnityEngine.SceneManagement;

public class Gate : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private Settings settings;
    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private UnityEvent<IntNumberDto> onScoreOutputed;
    [SerializeField] private UnityEvent<IntNumberDto> onTotalScoreOutputed;
    [SerializeField] private UnityEvent<IntNumberDto> onVHSOutputed;
    [SerializeField] private List<Levels> items;

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
            WriteProgress();
        }
    }
    private void Start()
    {
        settings.Unpause();
    }

    public void WriteProgress()
    {
        using (StreamReader r = new StreamReader("Levels.json"))
        {
            string json = r.ReadToEnd();
            items = JsonConvert.DeserializeObject<List<Levels>>(json);
        }

        items[0].lvlOpen = true;
        items[0].VHS = scoreCounter.VHSCount;
        items[0].Score = scoreCounter.ScoreCount;

        string jsonString = JsonConvert.SerializeObject(items);
        using (StreamWriter outputFile = new StreamWriter("Levels.json"))
        {
            outputFile.WriteLine(jsonString);
        }
    }

}
