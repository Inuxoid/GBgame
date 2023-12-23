using System;
using UnityEngine;

namespace StateMachines.PlayerSM
{
    public class UnFall : MonoBehaviour
    {
        public PlayerSM sm;

        private void Awake()
        {
            sm = GetComponentInParent<PlayerSM>();

        }
        public void UnFallM()
        {
            sm.canMoveAfterFalling = true;
        }
    }
}