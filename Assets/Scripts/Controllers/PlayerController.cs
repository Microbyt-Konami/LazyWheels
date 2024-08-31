using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MicrobytKonami.LazyWheels.Input;
using UnityEngine.InputSystem.XR;
using UnityEngine.Events;

namespace MicrobytKonami.LazyWheels.Controllers
{
    [RequireComponent(typeof(CarController))]
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField, Header("Player")] public float EnergyStart { get; private set; }

        // Components
        private Transform myTransform;
        private CarController carController;
        private InputActions inputActions;
        [field: SerializeField, Header("Debug")] public float Energy { get; private set; }

        public bool IsMoving
        {
            get => carController.IsMoving;
            set => carController.IsMoving = value;
        }

        public void Die()
        {
            myTransform.position -= myTransform.position.x * Vector3.right;
            carController.IsMoving = true;
            carController.Mover(0);
            StartMode();
        }

        public void PlayerNoFuel()
        {
            // GameOver
            Debug.Log("PlayerNoFuel");

            GameController.Instance.GameOver();
        }

        public void ConsumEnergy(float energy)
        {
            Energy -= energy;
            if (Energy <= 0)
            {
                // GameOver
                Debug.Log("No energy");

                IsMoving = false;
                GameController.Instance.GameOver();
            }
        }

        public void PowerUpEnery(float energy)
        {
            Energy += energy;
        }

        private void Awake()
        {
            myTransform = GetComponent<Transform>();
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
            Energy = EnergyStart;
            print($"supportsAccelerometer {SystemInfo.supportsAccelerometer}");
            print($"Gamepad.current: {Gamepad.current != null}");
            print($"isMobilePlatform: {Application.isMobilePlatform}");
            //Gamepad.current
#if UNITY_ANDROID
            print("UNITY_ANDROID");
#endif
            StartMode();
        }

        // Update is called once per frame
        void Update()
        {
            if (carController.IsMoving && !carController.IsExploding)
            {
                Move();
            }
        }

        private void Move()
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

        void StartMode()
        {
            StartCoroutine(StartModeCourotine());
        }

        IEnumerator StartModeCourotine()
        {
            Debug.Log("Start Mode");

            float time = 3;
            carController.SetModoGhost();

            //carController.CarFade(1 / 4f);

            yield return new WaitForSecondsRealtime(time);

            //carController.CarFade(1);

            carController.SetModoGhost(false);

            Debug.Log("End Mode");
        }
    }
}