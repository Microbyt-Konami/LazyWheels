using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MicrobytKonami.System;

using MicrobytKonami.LazyWheels.Controllers;
using MicrobytKonami.LazyWheels.UI;
using System;
using static UnityEngine.EventSystems.EventTrigger;

namespace MicrobytKonami.LazyWheels.Managers
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [Header("References")]
        [SerializeField] private BuilderBlocks builderBlocks;
        [SerializeField] private PlayerController player;
        [SerializeField] private AudioSource inicioFXSound;

        [Header("Debug")]
        // [SerializeField] private bool _hasLevelStarted;
        // [SerializeField] private bool _hasLevelFinished;
        // [SerializeField] private bool _isGamePlaying;
        // [SerializeField] private bool _isGamePaused;
        [SerializeField] private bool _isGameOver = false;

        // public bool HasLevelStarted { get => _hasLevelStarted; set => _hasLevelStarted = value; }
        // public bool HasLevelFinished { get => _hasLevelFinished; set => _hasLevelFinished = value; }
        // public bool IsGamePlaying { get => _isGamePlaying; set => _isGamePlaying = value; }
        // public bool IsGamePaused { get => _isGamePaused; set => _isGamePaused = value; }
        public bool IsGameOver { get => _isGameOver; set => _isGameOver = value; }

        public BlockController FindBlockInY(float y) => builderBlocks.FindBlockInY(y);

        void OnEnable()
        {
            if (player.CarController is not null)
            {
                player.CarController.onCarNoFuel.AddListener(GameOver);
                player.CarController.OnCarFuelChange += Player_CarFuelChange;
                player.OnPlayerEnergyChange += Player_EnergyChangeHandler;
            }
        }

        void OnDisable()
        {
            if (player.CarController is not null)
            {
                player.CarController.onCarNoFuel.RemoveListener(GameOver);
                player.CarController.OnCarFuelChange -= Player_CarFuelChange;
                player.OnPlayerEnergyChange -= Player_EnergyChangeHandler;
            }
        }

        IEnumerator Start()
        {
            var counter = UIController.Instance.CounterInitial;

            counter.gameObject.SetActive(true);
            player.CarController.IsMoving = false;
            player.CarController.PlayStartMotorSound();
            yield return new WaitForSeconds(2);


            //yield return counter.StartCounter();
            yield return StartCoroutine(counter.StartCounterCoroutine());

            Debug.Log("Inicio FX");
            inicioFXSound.Play();
            player.CarController.PlayMotorSound();

            yield return new WaitForSeconds(2f);
            counter.gameObject.SetActive(false);

            //player.CarController.StopStartMotorSound();
            player.CarController.IsMoving = true;
        }

        private void GameOver()
        {
            if (_isGameOver)
                return;

            StopAllCarsMotors();
            player.IsStopCounterMeter = true;
            _isGameOver = true;
            player.SetModoGhost(true);
            UIController.Instance.ShowGameOver();
        }

        private void Player_CarFuelChange(float fuel, float fuelMax)
        {
            UIController.Instance.Info.SetFuel(fuel, fuelMax);
        }

        private void Player_EnergyChangeHandler(float energy, float energyMax)
        {
            UIController.Instance.Info.SetEnergy(energy, energyMax);
            if (energy < 0)
                GameOver();
        }

        private void StopAllCarsMotors()
        {
            var cars = FindObjectsOfType<CarController>();

            foreach (var car in cars)
                car.StopMotorSound();
        }
    }
}