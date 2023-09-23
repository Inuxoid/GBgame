using System.Collections.Generic;
using System.Linq;
using Meta.Upgrades.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Meta.Upgrades.View.Inventory
{
    public class UpgradeInventoryView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private List<SocketViewInInventory> socketsViews;
        [SerializeField] private List<UpgradeViewInInventory> upgradesViews;
        [SerializeField] private UpgradeViewInInventory selectedUpgradeViewInInventory;
        [SerializeField] private SocketViewInInventory selectedSocketViewInInventory;
        [SerializeField] private GameObject upgradeViewPrefab;
        [SerializeField] private Transform upgradeViewCont;
        
        public TextMeshProUGUI Description
        {
            get => description;
            set => description = value;
        }
        
        public List<SocketViewInInventory> SocketsViews
        {
            get => socketsViews;
            set => socketsViews = value;
        }
        
        public UpgradeViewInInventory SelectedUpgradeViewInShop
        {
            get => selectedUpgradeViewInInventory;
            set => selectedUpgradeViewInInventory = value;
        }
        
        public SocketViewInInventory SelectedSocketViewInInventory
        {
            get => selectedSocketViewInInventory;
            set => selectedSocketViewInInventory = value;
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
                //Debug.Log("sv");
                socketView.Setup(UpgradeInventory.GetInstance().Sockets.Find(e => e.ID == socketView.ID));
            }
        }

        public void LoadUpgrades()
        {
            description.text = "";
            foreach (var upgrade in UpgradeShop.GetInstance().Upgrades
                         .Where(u => u.UpgradeState != Upgrade.UpgradeStates.InShop))
            {
                bool upgradeViewExists = UpgradesViews.Any(upgradeView => upgradeView.Upgrade == upgrade);

                if (!upgradeViewExists)
                {
                    var newCard = Instantiate(upgradeViewPrefab, upgradeViewCont);
                    newCard.GetComponent<UpgradeViewInInventory>().Setup(upgrade);
                    UpgradesViews.Add(newCard.GetComponent<UpgradeViewInInventory>());
                }
            }
        }


        public void Select(UpgradeViewInInventory upgradeViewInInventory)
        {
            if (
                selectedSocketViewInInventory is null || 
                upgradeViewInInventory is null || 
                socketsViews is null
            ) return;

            if (upgradeViewInInventory.Upgrade.Ultimate != selectedSocketViewInInventory.Socket.IsUltimate)
            {
                description.text = "You can't place it here.";
                return;
            }

            foreach (var svin in socketsViews.Where(v => v?.Socket.Upgrade != null && v.Socket.Upgrade == upgradeViewInInventory.Upgrade))
            {
                svin.UnSetUpgrade();  
            }
            selectedSocketViewInInventory.SetUpgrade(upgradeViewInInventory.Upgrade);
            selectedSocketViewInInventory.Clear();
            upgradeViewInInventory.UnSelect();
        }
        
        public void Select(SocketViewInInventory socketViewInInventory)
        {
            selectedSocketViewInInventory = socketViewInInventory;
        }

        public void ResetEverything()
        {
            foreach (var svin in socketsViews)
            {
                svin.UnSetUpgrade();
                svin.Clear();
            }
        }
    }
}