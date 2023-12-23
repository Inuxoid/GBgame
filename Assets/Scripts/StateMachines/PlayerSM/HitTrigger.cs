using UnityEngine;

namespace StateMachines.PlayerSM
{
    public class HitTrigger : MonoBehaviour
    {
        [SerializeField] private PlayerSM playerSm;

        private void Awake()
        {
            playerSm = GetComponentInParent<PlayerSM>();
        }

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