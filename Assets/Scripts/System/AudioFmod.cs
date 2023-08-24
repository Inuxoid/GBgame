using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class AudioFmod : MonoBehaviour
{
    public enum FloorType {Stone, Steel, Acid}

    public FloorType curFloorType = FloorType.Stone;
    
    public void footstepsRun()
    {
        switch (curFloorType)
        {
            case FloorType.Stone:
                FMODUnity.RuntimeManager.PlayOneShot("event:/FootstepDefault");
                break;
            case FloorType.Steel:
                FMODUnity.RuntimeManager.PlayOneShot("event:/FootstepStell");
                break;
            case FloorType.Acid:
                FMODUnity.RuntimeManager.PlayOneShot("event:/FootstepAcid");
                break;
        }

    }

    public void crouch()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/footstep_crouch");
    }
    
     public void climb()

    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/climb");
    }

    public void door()

    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/door");
    }

    public void woosh()

    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/woosh");
    }

    public void wooshhand()

    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/wooshHand");
    }

    public void punch1_1()

    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/punch1-1");
    }

    public void death1_1()

    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/death1-1");
    }

    public void punchmob1_1()

    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/punchmob1-1");
    }

    public void deathscream()

    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/deathmob1-1");
    }

     public void deathanim1()

    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/deathanim1");
    }

    public void deathanim2()

    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/deathanim2");
    }

     public void deathanim3()

    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/deathanim3");
    }

        public void mobuh()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/mobuh");
    }

        public void mobfoot()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/mobfootstep");
    }

         public void WooshUltimate()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/woosh_ultimate");
    }

          public void Gun_shot()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/Gun_shot");
    }

          public void Shield()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/shield");
    }
}