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
            print($"Gamepad.current: {Gamepad.current?.enabled}");
            //Gamepad.current
        }

        // Update is called once per frame
        void Update()
        {
            print(
                $"acceleration: {Accelerometer.current?.acceleration?.x?.ReadValue()} {Accelerometer.current?.acceleration?.y.ReadValue()} {Accelerometer.current?.acceleration?.z.ReadValue()}");
            carController.Mover(inputActions.Player.Move.ReadValue<float>());
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