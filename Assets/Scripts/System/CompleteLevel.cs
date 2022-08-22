using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField] private int nextLvl;
    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadNext()
    {
        SceneManager.LoadScene(nextLvl);
    }
}
