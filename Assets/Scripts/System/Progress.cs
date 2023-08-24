using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Progress : MonoBehaviour
{
    [SerializeField] private EndLevel endLvl;
    [SerializeField] private int maxLevelScore;

    [SerializeField] public int spriteInd;
    [SerializeField] private Sprite empty;
    [SerializeField] private Sprite twentyFive;
    [SerializeField] private Sprite fifty;
    [SerializeField] private Sprite seventyFive;
    [SerializeField] private Sprite full;

    [SerializeField] private Image battery;

    public void GetSprite()
    {
        switch (Convert.ToDouble(endLvl.Score) / Convert.ToDouble(maxLevelScore) * 100)
        {
            case <= 10:
                battery.sprite = empty;
                spriteInd = 0;
                break;
            case > 10 and <= 25:
                battery.sprite = twentyFive;
                spriteInd = 1;
                break;
            case > 25 and <= 50:
                battery.sprite = fifty;
                spriteInd = 2;
                break;
            case > 50 and <= 75:
                battery.sprite = seventyFive;
                spriteInd = 3;
                break;
            case > 75:
                battery.sprite = full;
                spriteInd = 4;
                break;
        }
    }
}
