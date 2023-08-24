using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace System
{
    public class PressedButton : Button
    {
        public bool isPressed;
        public bool isReleased;
        public UnityEvent OnPressed;
        
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);
            isPressed = true;
            isReleased = false;
            OnPressed.Invoke();
            Debug.Log("Pressed");
        }

        // Button is released
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);
            isPressed = false;
            isReleased = true;
            Debug.Log("Released");
        }
    }
}