using System.Collections.Generic;
using Meta.Upgrades.Controller;
using UnityEngine;

namespace Meta.Upgrades.View.Inventory
{
    public class UpgradeInventoryView : MonoBehaviour
    {
        [SerializeField] private List<SocketView> socketsViews;
        [SerializeField] private List<UpgradeViewInInventory> upgradesViews;
        [SerializeField] private UpgradeViewInInventory selectedUpgradeViewInInventory;
        [SerializeField] private SocketView selectedSocketView;
        
        public List<SocketView> SocketsViews
        {
            get => socketsViews;
            set => socketsViews = value;
        }
        
        public UpgradeViewInInventory SelectedUpgradeViewInShop
        {
            get => selectedUpgradeViewInInventory;
            set => selectedUpgradeViewInInventory = value;
        }
        
        public SocketView SelectedSocketView
        {
            get => selectedSocketView;
            set => selectedSocketView = value;
        }
        
        public List<UpgradeViewInInventory> UpgradesViews
        {
            get => upgradesViews;
            set => upgradesViews = value;
        }

        private void Start()
        {
            foreach (var socketView in socketsViews)
            {
                Debug.Log("sv");
                socketView.Setup(UpgradeInventory.GetInstance().Sockets.Find(e => e.ID == socketView.ID));
            }

            foreach (var upgradeView in upgradesViews)
            {
                Debug.Log("uv");
                upgradeView.Setup(UpgradeShop.GetInstance().Upgrades.Find(e => e.ID == upgradeView.ID));
            }
        }
        
        public void Select(UpgradeViewInInventory upgradeViewInShop)
        {
            if (selectedSocketView is null) return;
            selectedSocketView.SetUpgrade(upgradeViewInShop.Upgrade);
            selectedSocketView.UnSelect();
            upgradeViewInShop.UnSelect();
        }
        
        public void Select(SocketView socketView)
        {
            selectedSocketView = socketView;
        }
    }
}