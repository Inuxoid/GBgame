using Meta.Upgrades.Controller;
using Meta.Upgrades.View.HUD;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.Upgrades.View.Inventory
{
    public class SocketViewInInventory : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private Socket socket;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI viewName;
        [SerializeField] private UpgradeInventoryView upgradeInventoryView;
        [SerializeField] private Button buttonSelect;
        [SerializeField] private GameObject board;
        [SerializeField] private SocketHUDView socketHUDView;

        public int ID
        {
            get => id;
            set => id = value;
        }

        public Socket Socket
        {
            get => socket;
            set => socket = value;
        }

        public void Setup(Socket socket)
        {
            this.socket = socket;
            //icon = Resources.Load<Image>(socket.Upgrade.IconPath);
            buttonSelect.onClick.AddListener(Select);
            upgradeInventoryView = FindObjectOfType<UpgradeInventoryView>();
        }

        public void Select()
        {
            if (upgradeInventoryView.SelectedSocketViewInInventory != null)
                upgradeInventoryView.SelectedSocketViewInInventory.Clear();
            board.SetActive(true);
            upgradeInventoryView.Select(this);
        }

        public void UnSetUpgrade()
        {
            socket.Upgrade.UpgradeState = Upgrade.UpgradeStates.InInventory;
            board.SetActive(false);
            icon.sprite = null;
            foreach (var svih in socketHUDView.socketViewsInHUD)
            {
                if (svih.upgrade == socket.Upgrade)
                {
                    svih.Clear();
                    socket.Upgrade = null;
                }
            }
            //viewName.text = "";
        }

        public void Clear()
        {
            board.SetActive(false);
            upgradeInventoryView.Select((SocketViewInInventory)null);
        }

        public void SetUpgrade(Upgrade upgrade)
        {
            upgrade.UpgradeState = Upgrade.UpgradeStates.InSocket;
            socket.Upgrade = upgrade;
            //viewName.text = socket.Upgrade.Name;
            
            socketHUDView.LoadUpgrades();
            
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
    }
}