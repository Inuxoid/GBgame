using UnityEngine;

namespace StateMachines.PlayerSM
{
    public class UnHardStop : MonoBehaviour
    {
        [SerializeField] private PlayerSM sm;

        public void UnStop()
        {
            sm.animator.SetBool("isHardStopped", false);
            sm.canMoveAfterFalling = true;
        }
    }
}