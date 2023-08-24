using UnityEngine;

namespace StateMachines.PlayerSM
{
    public class HitTrigger : MonoBehaviour
    {
        [SerializeField] private PlayerSM playerSm;
        
        public void Hit()
        {
            playerSm.AttackState.Hit();
        }

        public void StrongHit()
        {
            playerSm.StrongPunchState.Hit();
        }
    }
}