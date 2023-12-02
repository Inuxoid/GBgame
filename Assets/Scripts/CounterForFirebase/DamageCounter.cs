using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCounter : MonoBehaviour
{
    [SerializeField] private int totalHitsTaken;
    [SerializeField] string currentLevelName;

    private void Start()
    {
        StartLevel();
    }

    public void StartLevel()
    {
        totalHitsTaken = 0;
    }

    public void OnPlayerDamaged()
    {
        // Увеличиваем счетчик урона
        totalHitsTaken++;
    }

    public void EndLevel()
    {
        Parameter[] parameters = TakeData("false");
        Firebase.Analytics.FirebaseAnalytics.LogEvent("level_completed", parameters);
    }

    public void OnPlayerDeath()
    {
        Parameter[] parameters = TakeData("true");

        // Log the 'player_death' event with the parameters array
        Firebase.Analytics.FirebaseAnalytics.LogEvent("player_death", parameters);
    }

    private Parameter[] TakeData(string isDead)
    {
        Firebase.Analytics.Parameter[] parameters = {
        new Firebase.Analytics.Parameter("total_hits_taken", totalHitsTaken),
        new Firebase.Analytics.Parameter("level", currentLevelName),
        new Firebase.Analytics.Parameter("death", isDead)
        };

        return parameters;
    }
}
