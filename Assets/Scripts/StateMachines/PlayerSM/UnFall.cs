using System;
using UnityEngine;

namespace StateMachines.PlayerSM
{
    public class UnFall : MonoBehaviour
    {
        public PlayerSM sm;

        public void UnFallM()
        {
            sm.canMoveAfterFalling = true;
        }
    }
}