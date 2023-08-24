using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EleButton : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private MovingPlarform movingPlarform;
    [SerializeField] private GameObject newB;
    [Header("Settings")]
    [SerializeField] private bool done;
    [SerializeField] private bool isTrying;
    [SerializeField] private bool isOpening;
    [SerializeField] private float y;
    [SerializeField] private float x;
    [SerializeField] private UnityEvent onPushed;


    public bool IsOpened
    {
        get => done; set
        {
            if (value == true)
            {
                done = value;
                ColorChange();
                ChangeTrry();
            }
        }
    }

    private void ColorChange()
    {
        if (done)
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.green);
        }
        else
        {
            GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.yellow);
        }
    }

    private void ChangeTrry()
    {
        movingPlarform.Points[1] = newB.transform;
    }

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
        if (other.CompareTag("Player") && isTrying && !isOpening)
        {
            StartCoroutine(ButtonTimer());
        }
    }

    IEnumerator ButtonTimer()
    {
        isOpening = true;
        IsOpened = true;
        onPushed?.Invoke();
        yield return new WaitForSeconds(1f);
        isOpening = false;
        yield return null;
    }
}
