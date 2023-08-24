using UnityEngine;
using UnityEngine.SceneManagement;
namespace StateMachines.PlayerSM.States
{
    public class Dead : BaseState
    {
        private PlayerSM sm;

        public Dead(PlayerSM stateMachine) : base("Dead", stateMachine)
        {
            sm = ((PlayerSM)stateMachine);
        }
        
        public override void Enter()
        {
            base.Enter();
           // Debug.LogError("Deaaaaaaaaaaaaaaad");
            sm.DeathProc();
        }

        public override void UpdateLogic()
        {
            base.UpdateLogic();
            if (sm.playerInput.actions["F"].IsPressed())
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}