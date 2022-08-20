using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Clicker : MonoBehaviour
{
    public UnityEvent onClick;
    public void CLick()
    {
        this.onClick.Invoke();
    }
}
