using Unity.VisualScripting;
using UnityEngine;

namespace StateMachines.FoeSM.States
{
    public class Death<T> : BaseState where T : BaseFoeSm<T>
    {
        private readonly T sm;

        public Death(T stateMachine) : base($"FoeSM Death ({typeof(T)})", stateMachine)
        {
            sm = stateMachine;   
        }

        public override void Enter()
        {
            base.Enter();
            sm.scoreCounter.CountScore(300);
            sm.GetComponent<Collider>().transform.gameObject.layer = LayerMask.NameToLayer("dead");
            sm.animator.SetBool("isDead", true);
            if (sm.GetComponentInChildren<Outline>() is not null)
            sm.GetComponentInChildren<Outline>().OutlineColor = Color.clear;
            sm.canvas.SetActive(false);
            sm.GetComponent<Rigidbody>().isKinematic = true;
            sm.GetComponent<Collider>().enabled = false;
        }
    }
}