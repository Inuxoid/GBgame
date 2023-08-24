using System.Collections;
using StateMachines.PlayerSM;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ButtonWait : MonoBehaviour
    {
        [SerializeField] private bool isWaiting = false;
        [SerializeField] private PlayerSM playerSM;
        [SerializeField] private float delay = 0.5f;
        
        public void Wait()
        {
            if (!isWaiting)
            {
                playerSM.isJumpButtonWasPressed = true;
                isWaiting = true;
                StartCoroutine(Enable());
            }
        }

        private IEnumerator Enable()
        {
            yield return new WaitForSeconds(delay);
            playerSM.isJumpButtonWasPressed = false;
            isWaiting = false;
        }
    }
}
