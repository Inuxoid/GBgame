using System.Collections.Generic;
using System.Linq;
using Meta.Upgrades.View.Inventory;
using Unity.VisualScripting;
using UnityEngine;

namespace Meta.Upgrades.View.HUD
{
    public class SocketHUDView : MonoBehaviour
    { 
        [SerializeField] public List<SocketViewInHUD> socketViewsInHUD;
        [SerializeField] private List<SocketViewInInventory> socketViews;
        
        public void LoadUpgrades()
        {
            foreach (var socketViewInHUD in socketViewsInHUD)
            {
                var selectedSocketView = socketViews.FirstOrDefault(sv => sv.ID == socketViewInHUD.Id);

                if (selectedSocketView.Socket.Upgrade != null)
                {
                    socketViewInHUD.Clear();
                    socketViewInHUD.Load(selectedSocketView.Socket.Upgrade);
                }
                else if (selectedSocketView.Socket.Upgrade == null)
                {
                    socketViewInHUD.Clear();
                }
            }
        }
    }
}