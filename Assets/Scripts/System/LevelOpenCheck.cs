using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelOpenCheck : MonoBehaviour
{
    [SerializeField] private List<Levels> items;
    [SerializeField] private Button buttonLvl1;
    public void CheckLevels()
    {
        using (StreamReader r = new StreamReader("Levels.json"))
        {
            string json = r.ReadToEnd();
            items = JsonConvert.DeserializeObject<List<Levels>>(json);
        }
        buttonLvl1.interactable = items[0].lvlOpen;
        //Debug.Log(items[0].lvlOpen);
    }

    private void Start()
    {
        CheckLevels();
    }
}

public class Levels
{
    public bool lvlOpen { get; set; }
    public int VHS { get; set; }
    public int Score { get; set; }
}