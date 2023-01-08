using Meta.Upgrades.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.Upgrades.View.Inventory
{
    public class UpgradeViewInInventory : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private Upgrade upgrade;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI viewName;
        [SerializeField] private GameObject board;
        [SerializeField] private Button buttonSelect;
        [SerializeField] private UpgradeInventoryView upgradeInventoryView;
        
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
            buttonSelect.onClick.AddListener(Select);
            this.upgrade = upgrade;
            viewName.text = upgrade.Name;
            icon = Resources.Load<Image>(upgrade.IconPath);
        }
        
        public void Select()
        {
            if (upgradeInventoryView.SelectedSocketView is null) return;
            board.SetActive(true);
            upgradeInventoryView.Select(this);
        }

        public void UnSelect()
        {
            upgradeInventoryView.Select((UpgradeViewInInventory)null);
            board.SetActive(false);
        }
    }
}