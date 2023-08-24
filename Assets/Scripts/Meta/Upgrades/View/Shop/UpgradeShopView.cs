using System;
using System.Collections.Generic;
using System.Linq;
using Meta.Upgrades.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.Upgrades.View.Shop
{
    public class UpgradeShopView : MonoBehaviour
    {
        [SerializeField] private UpgradeViewInShop selectedUpgradeViewInShop;
        [SerializeField] private TextMeshProUGUI description;
        [SerializeField] private Button buyButton;
        [SerializeField] private WalletView walletView;
        [SerializeField] private GameObject upgradeViewPrefab;
        [SerializeField] private Transform upgradeViewCont;
        [SerializeField] private Upgrade.UpgradeTypes upgradeType;

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

        public void Load()
        {
            foreach (var upgrade in UpgradeShop.GetInstance().Upgrades.Where(u => u.UpgradeType == upgradeType))
            {
                var newCard = Instantiate(upgradeViewPrefab, upgradeViewCont);
                newCard.GetComponent<UpgradeViewInShop>().Setup(upgrade);
            }
            buyButton.onClick.AddListener(Buy);
            walletView.DrawMoney();
        }

        public void Clear()
        {
            foreach (Transform t in upgradeViewCont) {
                Destroy(t.gameObject);
            }
        }

        public void Select(UpgradeViewInShop upgradeViewInShop)
        {
            selectedUpgradeViewInShop = upgradeViewInShop;
        }

      
        private void Buy()
        {
            if (!UpgradeShop.GetInstance().Buy(selectedUpgradeViewInShop.Upgrade))
            {
                //Debug.Log($"Can't buy");
                return;
            }
            //Debug.Log($"Bought {selectedUpgradeViewInShop.Upgrade.Name}");
            walletView.DrawMoney();
            selectedUpgradeViewInShop.Select();
        }
    }
}