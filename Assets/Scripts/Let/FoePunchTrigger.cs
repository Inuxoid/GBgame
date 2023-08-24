using System.Collections;
using System.Collections.Generic;
using Let.Foes;
using UnityEngine;

public class FoePunchTrigger : MonoBehaviour
{
    [SerializeField] private Foe foe;

    public void TriggerPunch()
    {
        foe.PostDamage();
    }
}
