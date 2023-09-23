using System;
using Meta.Upgrades.Controller;
using StateMachines.PlayerSM;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Meta.Upgrades.View.HUD
{
    public class SocketViewInHUD : MonoBehaviour
    {
        public int Id;
        public PressedButton pressedButton;
        public Upgrade upgrade;
        public PlayerSM playerSm;
        public Image icon;
        public Image backUpIcon;
        
        public void Load(Upgrade upgrade)
        {
            this.upgrade = upgrade;
            pressedButton.OnPressed.AddListener(() => this.upgrade.Action(playerSm, this));
            this.upgrade.Activate(playerSm);
            
            Sprite sprite = Resources.Load<Sprite>(upgrade.IconPath);
            if (sprite != null) 
            {
                icon.sprite = sprite;
            }
            else 
            {
                Debug.LogError("Failed to load sprite from path: " + upgrade.IconPath);
            } 
        }

        public void Clear()
        {
            upgrade?.Deactivate(playerSm);
            upgrade = null;
            pressedButton.onClick.RemoveAllListeners();
            icon.sprite = backUpIcon.sprite; 
        }
    }
}