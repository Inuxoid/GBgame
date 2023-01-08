using Meta.Upgrades.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.Upgrades.View.Shop
{
    public class UpgradeViewInShop : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private Upgrade upgrade;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI viewName;
        [SerializeField] private TextMeshProUGUI cost;
        [SerializeField] private GameObject board;
        [SerializeField] private GameObject upgradePanel;
        [SerializeField] private GameObject[] upgradeGrades;
        [SerializeField] private Button buttonUpgrade;
        [SerializeField] private Button buttonSelect;
        [SerializeField] private UpgradeShopView upgradeShopView;

        public int ID
        {
            get => id;
            set => id = value;
        }

        public Upgrade Upgrade
        {
            get => upgrade;
            set => upgrade = value;
        }
        
        public void Setup(Upgrade upgrade)
        {
            buttonUpgrade.onClick.AddListener(Upg);
            buttonSelect.onClick.AddListener(Select);
            this.upgrade = upgrade;
            viewName.text = upgrade.Name;
            icon = Resources.Load<Image>(upgrade.IconPath);
            cost.text = $"Buy cost: {upgrade.BuyCost.ToString()}";
            
            if (upgrade.UpgradeState != Upgrade.UpgradeStates.InInventory) return;
            cost.gameObject.SetActive(false);
            upgradePanel.SetActive(true);
            DrawUpgrade(upgrade);
        }

        public void DrawUpgrade(Upgrade upgrade)
        {
            for (var i = 1; i <= upgrade.Level; i++)
            {
                upgradeGrades[i - 1].SetActive(true);
            }
        }

        private void Upg()
        {
            Debug.Log(UpgradeShop.GetInstance().Upgrade(upgrade)
                ? $"Upgraded {upgrade.Name} to level {upgrade.Level}"
                : $"Error while upgraded {upgrade.Name}");
            DrawUpgrade(upgrade);
        }

        public void Select()
        {
            if (upgradeShopView.SelectedUpgradeViewInShop != null)
                upgradeShopView.SelectedUpgradeViewInShop.UnSelect();
            upgradeShopView.Select(this);
            buttonUpgrade.gameObject.SetActive(upgrade.UpgradeState != Upgrade.UpgradeStates.InShop);
            upgradeShopView.Description.text = upgrade.Description;
            board.SetActive(true);
            if (upgrade.UpgradeState != Upgrade.UpgradeStates.InInventory) return;
            cost.gameObject.SetActive(false);
            upgradePanel.SetActive(true);
            DrawUpgrade(upgrade);
        }

        public void UnSelect()
        {
            upgradeShopView.Select(null);
            buttonUpgrade.gameObject.SetActive(false);
            board.SetActive(false);
        }
    }
}