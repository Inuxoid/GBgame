using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public void Death()
    {
        Debug.Log("I'm dead");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

