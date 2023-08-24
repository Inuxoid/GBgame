using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
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
    [SerializeField] private GameObject audioSource;
    [SerializeField] private PlayerInput playerInput;
    
    public void Death()
    {
        //Debug.Log("I'm dead");
        Ragdoll.SetActive(true);
        Instantiate(Ragdoll, PlayerModel.transform.position, PlayerModel.transform.rotation);
        Destroy(playerMovement);
        Destroy(liveCycle);
        Destroy(model);
        Destroy(groundChecker);
        Destroy(airChecker);
        Destroy(audioSource);
        deathPanel.SetActive(true);
    }
}

