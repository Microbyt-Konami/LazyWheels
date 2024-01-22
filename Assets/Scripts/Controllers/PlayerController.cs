using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MicrobytKonami.LazyWheels.Input;

namespace MicrobytKonami.LazyWheels.Controllers
{
    [RequireComponent(typeof(CarController))]
    public class PlayerController : MonoBehaviour
    {
        // Components
        private CarController carController;
        private InputActions inputActions;

        private void Awake()
        {
            carController = GetComponent<CarController>();
            inputActions = new InputActions();
            //inputActions.Player.Move.performed += ctx => carController.Mover(ctx.ReadValue<float>());
        }

        private void OnEnable()
        {
            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        // Start is called before the first frame update
        void Start()
        {
            print($"supportsAccelerometer {SystemInfo.supportsAccelerometer}");
            print($"Gamepad.current: {Gamepad.current != null}");
            print($"isMobilePlatform: {Application.isMobilePlatform}");
            //Gamepad.current
#if UNITY_ANDROID
            print("UNITY_ANDROID");
#endif
        }

        // Update is called once per frame
        void Update()
        {
#if UNITY_WEBGL || UNITY_ANDROID || UNITY_IOS
            //print($"acceleration: {Accelerometer.current?.acceleration?.x?.ReadValue()} {Accelerometer.current?.acceleration?.y.ReadValue()} {Accelerometer.current?.acceleration?.z.ReadValue()}");
            //print($"acceleration: {UnityEngine.Input.acceleration.x} {UnityEngine.Input.acceleration.y} {UnityEngine.Input.acceleration.z}");
#endif
            float inputX = inputActions.Player.Move.ReadValue<float>();

            if (Gamepad.current == null)
                if (inputX == 0 && ApplicationEx.supportsAccelerometer)
                    inputX = inputActions.Player.MoveAcceleration.ReadValue<Vector3>().x;
            carController.Mover(inputX);
        }

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    print($"OnTriggerEnter2D: {collision.gameObject.name}");
        //}

        //private void OnCollisionEnter2D(Collision2D collision)
        //{
        //    print($"OnCollisionEnter2D: {collision.gameObject.name}");
        //}
    }
}