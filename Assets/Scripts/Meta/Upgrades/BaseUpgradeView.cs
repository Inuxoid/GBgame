using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Meta.Upgrades
{
    public class BaseUpgradeView : MonoBehaviour
    {
        private UpgradeShop upgradeShop;
        [SerializeField] private int id;
        [SerializeField] private Button buttonUpgrade;
        [SerializeField] private Button buttonSet;
        [SerializeField] private Button buttonBuy;
        [SerializeField] private BaseUpgrade baseUpgrade;

        private void Start()
        {
            upgradeShop = UpgradeShop.GetInstance();
            baseUpgrade = upgradeShop.GetUpgrades().FirstOrDefault(e => e.id == id);
            if (baseUpgrade == null)
            {
                Debug.LogError("Can't find upgrade =(");
            }
            else
            {
                buttonBuy.onClick.AddListener(BuyThis);
                if (baseUpgrade.isBought)
                {
                    buttonBuy.gameObject.SetActive(false);
                }
            }
        }

        private void BuyThis()
        {
            if (!upgradeShop.Buy(baseUpgrade))
            {
                Debug.Log($"Can't buy");
                return;
            }
            buttonSet.onClick.AddListener(SetThis);
            buttonUpgrade.onClick.AddListener(UpgradeThis);
            buttonBuy.gameObject.SetActive(false);
            Debug.Log($"Bought {baseUpgrade.id}");
        }

        private void UpgradeThis()
        {
            Debug.Log(upgradeShop.Upgrade(baseUpgrade)
                ? $"Upgraded {baseUpgrade.id} to lvl {baseUpgrade.level}"
                : $"Can't be upgraded");
        }

        private void SetThis()
        {
            upgradeShop.SetUpgrade(baseUpgrade);
            buttonSet.onClick.RemoveListener(SetThis);
            buttonSet.onClick.AddListener(UnsetThis);
            Debug.Log($"Set {baseUpgrade.id}");
        }

        private void UnsetThis()
        {
            upgradeShop.UnsetUpgrade(baseUpgrade);
            buttonSet.onClick.RemoveListener(UnsetThis);
            buttonSet.onClick.AddListener(SetThis);
            Debug.Log($"Unset {baseUpgrade.id}");
        }
    }
}