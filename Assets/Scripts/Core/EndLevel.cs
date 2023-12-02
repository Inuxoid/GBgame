using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using StateMachines.PlayerSM;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class EndLevel : MonoBehaviour
{
    [SerializeField] private GameObject scorePanel;
    [SerializeField] private ScoreCounter scoreCounter;
    [SerializeField] private LiveCycle liveCycle;
    [SerializeField] private Progress progress;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private int score;
    [SerializeField] private int lvl;
    [SerializeField] private List<Levels> items;
    [SerializeField] private UnityEvent onLevelEnd;
    public int Score { get => score; set => score = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerSM>())
        {
            scorePanel.SetActive(true);
            onLevelEnd.Invoke();
            //Score = scoreCounter.VHSCount * 3 + (int)liveCycle.Hp * 10;
            //textScore.text = Score.ToString();
            //progress.GetSprite();
            //CheckLevels();
        }
    }


    public void CheckLevels()
    {
        using (StreamReader r = new StreamReader("Levels.json"))
        {
            string json = r.ReadToEnd();
            items = JsonConvert.DeserializeObject<List<Levels>>(json);
        }

        items[lvl + 1].lvlOpen = true;

        string jsonString = JsonConvert.SerializeObject(items);
        using (StreamWriter outputFile = new StreamWriter("Levels.json"))
        {
            outputFile.WriteLine(jsonString);
        }
    }
}
