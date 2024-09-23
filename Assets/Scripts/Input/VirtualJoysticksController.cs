using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

namespace MicrobytKonami.LazyWheels.Input
{
    public class VirtualJoysticksController : MonoBehaviour
    {
        [SerializeField] private float sensitivity = 0.1f;
        [SerializeField] private OnVirtualJoysticksScreenControl left;
        [SerializeField] private OnVirtualJoysticksScreenControl right;

        private void OnEnable()
        {
#if UNITY_EDITOR
            TouchSimulation.Enable();
#endif
            EnhancedTouchSupport.Enable();
            Touch.onFingerDown += OnFingerDown;
            Touch.onFingerUp += OnFingerUp;
        }

        private void OnDisable()
        {
#if UNITY_EDITOR
            TouchSimulation.Disable();
#endif
            EnhancedTouchSupport.Disable();
            Touch.onFingerDown -= OnFingerDown;
            Touch.onFingerUp -= OnFingerUp;
        }

        private void OnFingerDown(Finger finger)
        {
            SendValueToControl(finger, sensitivity);
        }

        private void OnFingerUp(Finger finger)
        {
            SendValueToControl(finger, 0.0f);
        }

        private void SendValueToControl(Finger finger, float value)
        {
            var viewport = Camera.main.ScreenToViewportPoint(finger.screenPosition);

            Debug.Log($"viewport: {viewport}");
            if (viewport.x < 0.5)
                left.SendValue(value);
            else
                right.SendValue(value);
        }
    }
}
