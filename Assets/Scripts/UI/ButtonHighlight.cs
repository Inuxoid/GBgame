using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Reflection;

namespace TMPro
{
    public class ButtonHighlight : MonoBehaviour
    {
        public TextMeshProUGUI text;

        public void Highlight()
        {
            var highlight = typeof(TextMeshProUGUI).GetMethod("SetOutlineThickness",
                                                              BindingFlags.Instance |
                                                              BindingFlags.Public |
                                                              BindingFlags.NonPublic);
            highlight?.Invoke(text, parameters: new object[] { 0.3f });
        }

        public void Downlight()
        {
            var highlight = typeof(TextMeshProUGUI).GetMethod("SetOutlineThickness",
                                                              BindingFlags.Instance |
                                                              BindingFlags.Public |
                                                              BindingFlags.NonPublic);
            highlight?.Invoke(text, parameters: new object[] { 0.055f });
        }

        public void Down()
        {
            var highlight = typeof(TextMeshProUGUI).GetMethod("SetFaceColor",
                                                              BindingFlags.Instance |
                                                              BindingFlags.Public |
                                                              BindingFlags.NonPublic);
            highlight?.Invoke(text, parameters: new object[] { new Color32(135, 0, 166, 255) });
        }

        public void Up()
        {
            var highlight = typeof(TextMeshProUGUI).GetMethod("SetFaceColor",
                                                              BindingFlags.Instance |
                                                              BindingFlags.Public |
                                                              BindingFlags.NonPublic);
            highlight?.Invoke(text, parameters: new object[] { new Color32(216, 0, 255, 255) });
        }
    }
}