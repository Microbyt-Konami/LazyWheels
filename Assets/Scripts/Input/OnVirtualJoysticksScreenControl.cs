using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace MicrobytKonami.LazyWheels.Input
{
    public class OnVirtualJoysticksScreenControl : OnScreenControl
    {
        [InputControl(layout = "Button")]
        [SerializeField] private string m_ControlPath;

        protected override string controlPathInternal { get => m_ControlPath; set => m_ControlPath = value; }

        public void SendValue(float value) => SendValueToControl(value);
    }
}
