using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject Ragdoll;
    public GameObject Player;
    public void Death()
    {
        Debug.Log("I'm dead");
        Player.SetActive(false);
        Ragdoll.SetActive(true);
        Instantiate(Ragdoll, Player.transform.position, Player.transform.rotation);
        ///SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

