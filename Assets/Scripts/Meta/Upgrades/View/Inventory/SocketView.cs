using Meta.Upgrades.Controller;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.Upgrades.View.Inventory
{
    public class SocketView : MonoBehaviour
    {
        [SerializeField] private int id;
        [SerializeField] private Socket socket;
        [SerializeField] private Image icon;
        [SerializeField] private TextMeshProUGUI viewName;
        [SerializeField] private UpgradeInventoryView upgradeInventoryView;
        [SerializeField] private Button buttonSelect;
        [SerializeField] private GameObject board;
        [SerializeField] private Upgrade upgrade;

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
        }

        public void Select()
        {
            if (upgradeInventoryView.SelectedSocketView != null)
                upgradeInventoryView.SelectedSocketView.UnSelect();
            board.SetActive(true);
            upgradeInventoryView.Select(this);
        }

        public void UnSelect()
        {
            upgradeInventoryView.Select((SocketView)null);
            board.SetActive(false);
        }

        public void SetUpgrade(Upgrade upgrade)
        {
            this.upgrade = upgrade;
            viewName.text = upgrade.Name;
        }
    }
}