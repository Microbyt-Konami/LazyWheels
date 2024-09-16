using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;
using UnityEngine.Events;
using System;

using MicrobytKonami.LazyWheels.Input;
using MicrobytKonami.LazyWheels.Managers;

namespace MicrobytKonami.LazyWheels.Controllers
{
    public delegate void OnPlayerEnergyChangeHandler(float energy, float energyMax);

    [RequireComponent(typeof(CarController))]
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField, Header("Player")] public float EnergyStart { get; private set; }
        [SerializeField] private float energyWhenExplode;
        [SerializeField] private AudioSource lostCarSoundFX;
        [SerializeField] private AudioSource catchItSoundFX;

        // Components
        private Transform myTransform;
        public CarController CarController { get; private set; }
        private InputActions inputActions;
        [field: SerializeField, Header("Debug")] public float Energy { get; private set; }
        [field: SerializeField] public float Meters { get; private set; }
        [field: SerializeField] public bool IsStopCounterMeter { get; set; } = false;

        private int idPowerUp;

        //Events
        public event OnPlayerEnergyChangeHandler OnPlayerEnergyChange;

        public void Die()
        {
            lostCarSoundFX.Play();
            ConsumEnergy(energyWhenExplode);
            myTransform.position -= myTransform.position.x * Vector3.right;
            CarController.IsMoving = true;
            CarController.Mover(0);
            StartMode();
        }

        public void SetModoGhost(bool ghost = true)
        {
            CarController.SetModoGhost(ghost);
        }

        public void ConsumEnergy(float energy)
        {
            Energy -= energy;
            if (Energy <= 0)
            {
                // GameOver
                Debug.Log("No energy");

                CarController.IsMoving = false;
            }
            OnEnergyChange();
        }

        public void PowerUpEnery(float energy)
        {
            Energy += energy;
            OnEnergyChange();
        }

        private void Awake()
        {
            myTransform = GetComponent<Transform>();
            CarController = GetComponent<CarController>();
            inputActions = new InputActions();
            //inputActions.Player.Move.performed += ctx => carController.Mover(ctx.ReadValue<float>());

            // ids
            idPowerUp = LayerMask.NameToLayer("PowerUp");
        }

        private void OnEnable()
        {
            inputActions.Enable();
            CarController.OnCatchIt += Player_OnCatchIt;
        }

        private void OnDisable()
        {
            inputActions.Disable();
            CarController.OnCatchIt -= Player_OnCatchIt;
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
            // CarController.PlayStartMotorSound();
            // StartMode();
            OnEnergyChange();
        }

        // Update is called once per frame
        void Update()
        {
            if (CarController.IsMoving)
            {
                if (!CarController.IsExploding)
                    Move();
                if (!IsStopCounterMeter)
                    Meters = myTransform.position.y;
            }
        }

        private void OnEnergyChange()
        {
            OnPlayerEnergyChange?.Invoke(Energy, EnergyStart);
        }

        private void Player_OnCatchIt() => catchItSoundFX.Play();

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
            CarController.Mover(inputX);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == idPowerUp)
                CatchPowerUp(collision.gameObject);
        }

        private void CatchPowerUp(GameObject powerUp)
        {
            var powerUpData = powerUp.GetComponent<PowerUp>();

            if (powerUpData is not null)
            {
                CarController.CatchIt(powerUp);
                if (powerUpData.PowerUpEnergy > 0)
                {
                    PowerUpEnery(powerUpData.PowerUpEnergy);
                }
                else if (powerUpData.PowerUpEnergy < 0)
                    ConsumEnergy(-powerUpData.PowerUpEnergy);
            }
        }

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
            CarController.SetModoGhost();

            //carController.CarFade(1 / 4f);

            yield return new WaitForSecondsRealtime(time);

            //carController.CarFade(1);

            CarController.SetModoGhost(false);

            CarController.PlayMotorSound();

            Debug.Log("End Mode");
        }
    }
}