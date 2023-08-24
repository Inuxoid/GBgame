using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace StateMachines.PlayerSM
{
    public class JumpForceHelper : MonoBehaviour
    {
        public PlayerSM playerSm;
        public float jumpHeight = 1f;
        public float jumpForwardForceForwardMult = 1f;
        public float jumpForwardHeight = 1f;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponentInParent<PlayerSM>())
            {
                playerSm.jumpHeight = jumpHeight;
                playerSm.jumpForwardForceForwardMult = jumpForwardForceForwardMult;
                playerSm.jumpForwardHeight = jumpForwardHeight;
            }
        }
    }
}