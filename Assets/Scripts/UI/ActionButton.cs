using System;
using StateMachines;
using StateMachines.PlayerSM;
using StateMachines.PlayerSM.States;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ActionButton : MonoBehaviour, IPlayerStateObserver
    {
        public Button playerButton;
        public Sprite crouchIcon;
        public Sprite hideIcon;
        public Sprite actionIcon;
        private bool isIdle;
        private bool isActNear;
        private bool isHideNear;
        private bool isHiding;

        public void OnPlayerStateChanged(BaseState newState)
        {
            isIdle = newState is Idle or Run;
            isHiding = newState is HideIdle or HideCrouch;
        }
        
        public void OnActionNear(bool isNear)
        {
            isActNear = isNear;
            if (isIdle && isActNear)
            {
                playerButton.image.sprite = actionIcon; 
            }
            else if (!isActNear)
            {
                playerButton.image.sprite = crouchIcon;
            }
        }
        
        public void OnHideNear(bool isNear)
        {
            isHideNear = isNear;
            if (isIdle && (isHideNear || isHiding))
            {
                playerButton.image.sprite = hideIcon; 
            }
            else if (!(isHideNear || isHiding))
            {
                playerButton.image.sprite = crouchIcon;
            }
        }
    }
    
}