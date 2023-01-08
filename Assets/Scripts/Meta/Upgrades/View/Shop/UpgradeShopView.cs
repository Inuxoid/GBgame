using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.Upgrades.View.Shop
{
    public class UpgradeShopView : MonoBehaviour
    {
        [SerializeField] private List<UpgradeViewInShop> upgradesViews;
        [SerializeField] private UpgradeViewInShop selectedUpgradeViewInShop;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private Button buyButton;

        public TextMeshProUGUI Description
        {
            get => description;
            set => description = value;
        }

        public UpgradeViewInShop SelectedUpgradeViewInShop
        {
            get => selectedUpgradeViewInShop;
            set => selectedUpgradeViewInShop = value;
        }

        public List<UpgradeViewInShop> UpgradesViews
        {
            get => upgradesViews;
            set => upgradesViews = value;
        }

        private void Start()
        {
            foreach (var upgradeView in upgradesViews)
            {
                upgradeView.Setup(UpgradeShop.GetInstance().Upgrades.Find(e => e.ID == upgradeView.ID));
            }
            buyButton.onClick.AddListener(Buy);
        }

        public void Select(UpgradeViewInShop upgradeViewInShop)
        {
            selectedUpgradeViewInShop = upgradeViewInShop;
        }

      
        private void Buy()
        {
            if (!UpgradeShop.GetInstance().Buy(selectedUpgradeViewInShop.Upgrade))
            {
                Debug.Log($"Can't buy");
                return;
            }
            Debug.Log($"Bought {selectedUpgradeViewInShop.Upgrade.Name}");
            selectedUpgradeViewInShop.Select();
        }
    }
}