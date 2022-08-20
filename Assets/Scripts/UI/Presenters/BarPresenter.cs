using Dto;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Presenters
{
    public class BarPresenter : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Image bar;
        [SerializeField] private Slider sBar;

        public void Draw(IntNumberDto dto) => this.bar.fillAmount = dto.value;
        public void DrawFloat(FloatNumberDto dto) => this.bar.fillAmount = dto.value;
        public void DrawScroll(FloatNumberDto dto) => this.sBar.value = dto.value;
    }
}
