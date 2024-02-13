using System;
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

        public void Draw(IntNumberDto dto) => this.uiText.text = (Math.Round((float)dto.value, 1)).ToString();
        public void DrawIntSuf(IntNumberDto dto) => this.uiText.text = (Math.Round((float)dto.value, 1)).ToString() + this.suffix;
        public void DrawWithSuffix(FloatNumberDto dto) => this.uiText.text = (Math.Round(dto.value, 1)).ToString() + this.suffix;
        public void DrawHP(FloatNumberDto dto) => this.uiText.text = (Math.Round(dto.value, 1)).ToString();
    }
}