using System;
using System.Collections;
using System.Collections.Generic;
using StateMachines.PlayerSM;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class DoorButton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject door;
    [SerializeField] private DoorButton otherButton;
    [Header("Settings")]
    [SerializeField] private bool isNotWorking;
    [SerializeField] private bool isOpened;
    [SerializeField] private bool isTrying;
    [SerializeField] private bool isOpening;
    [SerializeField] private float y;
    [SerializeField] private float x;
    [SerializeField] private UnityEvent onPushed;
    [SerializeField] private PlayerInput playerInput;

    private void Start()
    {
        playerInput = FindObjectOfType<PlayerInput>();
    }

    public bool IsOpened
    {
        get => isOpened; set
        {
            isOpened = value;
            ColorChange();
            DoorPositionChange();
        }
    }

    private void ColorChange()
    {
        if (isOpened)
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
            FMODUnity.RuntimeManager.PlayOneShot("event:/button_ok");
        }

        else
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);
            FMODUnity.RuntimeManager.PlayOneShot("event:/button_ok_revers");
        }

        if (isNotWorking)
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.red);
        }
    }

    private void DoorPositionChange()
    {
        if (isOpened)
        {
            door.transform.position = new Vector3(door.transform.position.x - x, door.transform.position.y - y);
        }
        else
        {
            door.transform.position = new Vector3(door.transform.position.x + x, door.transform.position.y + y);
        }
    }

    private void Update()
    {
        if (playerInput.actions["Action"].IsPressed())
        {
            isTrying = true;
        }

        if (!playerInput.actions["Action"].IsPressed())
        {
            isTrying = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerSM>().canAction = true;
        }
        if (other.CompareTag("Player") && isTrying && !isOpening)
        {
            StartCoroutine(ButtonTimer());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInParent<PlayerSM>().canAction = false;
        }
    }

    IEnumerator ButtonTimer()
    {
        isOpening = true;
        IsOpened = !IsOpened;
        otherButton.IsOpened = !otherButton.IsOpened;
        onPushed?.Invoke();
        yield return new WaitForSeconds(1f);
        isOpening = false;
        yield return null;
    }
}


