using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLoader : MonoBehaviour
{
    [SerializeField] private List<Levels> items;
    [SerializeField] private Button buttonLvl1;
    [SerializeField] private ScoreCounter scoreCounter;

    void Start()
    {
        CheckLevels();
    }

    public void CheckLevels()
    {
        using (StreamReader r = new StreamReader("Levels.json"))
        {
            string json = r.ReadToEnd();
            items = JsonConvert.DeserializeObject<List<Levels>>(json);
        }
        scoreCounter.LoadVHS(items[0].VHS);
        scoreCounter.CountScore(items[0].Score);
    }
}
