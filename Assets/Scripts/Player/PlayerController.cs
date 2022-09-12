using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject Ragdoll;
    public GameObject Player;
    public GameObject PlayerModel;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private bool isDead;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private AirChecker airChecker;
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private LiveCycle liveCycle;
    [SerializeField] private GameObject model;

    public void Death()
    {
        Debug.Log("I'm dead");
        Ragdoll.SetActive(true);
        Instantiate(Ragdoll, PlayerModel.transform.position, PlayerModel.transform.rotation);
        Destroy(playerMovement);
        Destroy(liveCycle);
        Destroy(model);
        Destroy(groundChecker);
        Destroy(airChecker);
        deathPanel.SetActive(true);
        isDead = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

}

