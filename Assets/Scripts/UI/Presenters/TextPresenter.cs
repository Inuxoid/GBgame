using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Dto;

namespace Presenters
{
    public class TextPresenter : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI uiText;

        [Header("Settings")]
        [SerializeField] private string suffix;

        public void Draw(IntNumberDto dto) => this.uiText.text = dto.value.ToString();
        public void DrawIntSuf(IntNumberDto dto) => this.uiText.text = dto.value.ToString() + this.suffix;
        public void DrawWithSuffix(FloatNumberDto dto) => this.uiText.text = dto.value.ToString() + this.suffix;
        public void DrawHP(FloatNumberDto dto) => this.uiText.text = (dto.value).ToString();
    }
}