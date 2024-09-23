using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace MicrobytKonami.LazyWheels.Input
{
    [AddComponentMenu("Input/On-Screen Sensitive Button")]
    public class OnScreenButtonOnScreenSensitiveButton : OnScreenControl, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private float sensitivity = 0.1f;
        [InputControl(layout = "Button")]
        [SerializeField] private string m_ControlPath;

        protected override string controlPathInternal { get => m_ControlPath; set => m_ControlPath = value; }

        public void OnPointerDown(PointerEventData eventData)
        {
            SendValueToControl(sensitivity);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            SendValueToControl(0.0f);
        }
    }
}
