using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MicrobytKonami.System;

using MicrobytKonami.LazyWheels.Controllers;
using MicrobytKonami.LazyWheels.UI;

namespace MicrobytKonami.LazyWheels.Managers
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        [Header("References")]
        [SerializeField] private BuilderBlocks builderBlocks;
        [SerializeField] private PlayerController player;

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

        public void LoadCarIAs(ICollection<CarIAController> carsIA)
        {
        }

        public void MoveCarIA(CarIAController carIA)
        {

        }

        void OnEnable()
        {
            if (player.CarController is not null)
            {
                player.CarController.onCarNoFuel.AddListener(GameOver);
                player.OnPlayerEnergyChange += Player_EnergyChangeHandler;
            }
        }

        void OnDisable()
        {
            if (player.CarController is not null)
            {
                player.CarController.onCarNoFuel.RemoveListener(GameOver);
                player.OnPlayerEnergyChange -= Player_EnergyChangeHandler;
            }
        }

        IEnumerator Start()
        {
            player.CarController.IsMoving = false;
            player.CarController.PlayStartMotorSound();
            yield return new WaitForSeconds(5f);
            player.CarController.StopStartMotorSound();
            player.CarController.PlayMotorSound();
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

        private void Player_EnergyChangeHandler(float energy, float energyMax)
        {
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
