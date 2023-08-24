using Dto;
using Meta.Upgrades.Controller;
using Presenters;
using UnityEngine;

namespace Meta.Upgrades.View
{
    public class WalletView : MonoBehaviour
    {
        [SerializeField] private TextPresenter textPresenter1;
        [SerializeField] private TextPresenter textPresenter2;
        
        public void DrawMoney()
        {
            var moneyDto = new IntNumberDto();
            moneyDto.value = Wallet.GetInstance().Balance;
            textPresenter1.DrawIntSuf(moneyDto);
            textPresenter2.DrawIntSuf(moneyDto);
        }
    }
}