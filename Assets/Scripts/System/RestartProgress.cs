using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class RestartProgress : MonoBehaviour
{
    [SerializeField] private List<Levels> items;
    public void RestartProg()
    {
        using (StreamReader r = new StreamReader("Levels.json"))
        {
            string json = r.ReadToEnd();
            items = JsonConvert.DeserializeObject<List<Levels>>(json);
        }

        foreach (Levels items in items)
        {
            items.lvlOpen = false;
            items.indSpritesBattery = 0;
        }

        items[0].lvlOpen = true;

        string jsonString = JsonConvert.SerializeObject(items);
        using (StreamWriter outputFile = new StreamWriter("Levels.json"))
        {
            outputFile.WriteLine(jsonString);
        }
    }
}
