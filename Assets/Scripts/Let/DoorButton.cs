using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject door;
    [SerializeField] private DoorButton otherButton;
    [Header("Settings")]
    [SerializeField] private bool isOpened;
    [SerializeField] private bool isTrying;

    public bool IsOpened { get => isOpened; set => isOpened = value; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isTrying = true;
        }

        if (Input.GetKeyUp(KeyCode.E))
        {
            isTrying = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isTrying)
        {
            if (!IsOpened && !otherButton.IsOpened)
            {
                door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y - 5f);
                otherButton.IsOpened = true;
                IsOpened = true;
            }
        }
    }
}
