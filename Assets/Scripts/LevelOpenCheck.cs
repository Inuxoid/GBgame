using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelOpenCheck : MonoBehaviour
{
    [SerializeField] private Button buttonLvl1;
    [SerializeField] private Button buttonLvl2;
    [SerializeField] private Button buttonLvl3;

    [SerializeField] private Image batteryLvl1;
    [SerializeField] private Image batteryLvl2;
    [SerializeField] private Image batteryLvl3;

    [SerializeField] private Sprite[] spritesBattery;

    [SerializeField] private List<Levels> items;
    public void CheckLevels()
    {
        using (StreamReader r = new StreamReader("Levels.json"))
        {
            string json = r.ReadToEnd();
            items = JsonConvert.DeserializeObject<List<Levels>>(json);
        }

        buttonLvl1.interactable = items[0].lvlOpen;
        buttonLvl2.interactable = items[1].lvlOpen;
        buttonLvl3.interactable = items[2].lvlOpen;

        batteryLvl1.sprite = spritesBattery[items[0].indSpritesBattery];
        batteryLvl2.sprite = spritesBattery[items[1].indSpritesBattery];
        batteryLvl3.sprite = spritesBattery[items[2].indSpritesBattery];
    }
}


public class Levels
{
    public bool lvlOpen { get; set; }
    public int indSpritesBattery { get; set; }
}
