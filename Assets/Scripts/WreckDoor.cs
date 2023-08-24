using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WreckDoor : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private PlayerInput playerInput;

    private void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (playerInput.actions["Attack"].IsPressed())
        {
            animator.SetBool("Play", true);
        }

    }
}