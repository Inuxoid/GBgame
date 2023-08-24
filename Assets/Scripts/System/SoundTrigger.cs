using System;
using System.Collections;
using System.Collections.Generic;
using StateMachines.PlayerSM;
using UnityEngine;

public class SoundTrigger : MonoBehaviour
{
    public AudioFmod audioFmod;

    public AudioFmod.FloorType floorType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<PlayerSM>())
        {
            audioFmod.curFloorType = floorType;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponentInParent<PlayerSM>() && audioFmod.curFloorType == floorType)
        {
            audioFmod.curFloorType = AudioFmod.FloorType.Stone;
        }
    }

}
