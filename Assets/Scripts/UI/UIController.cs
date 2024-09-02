using MicrobytKonami.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using MicrobytKonami.LazyWheels.Controllers;

namespace MicrobytKonami.LazyWheels.UI
{
    public class UIController : MonoBehaviourSingleton<UIController>
    {
        [Header("References")]
        [SerializeField] private PlayerController player;

        [Header("UI")]
        [SerializeField] private GameObject panelGameOver;
        [SerializeField] private Image fuelFill;
        [SerializeField] private Slider fuelSlider;
        [SerializeField] private Image energyFill;
        [SerializeField] private Slider energySlider;
        [SerializeField] private TextMeshProUGUI distanceText;

        // Components

        public void ShowGameOver()
        {
            panelGameOver.SetActive(true);
        }

        // Start is called before the first frame update
        void Start()
        {
            panelGameOver.SetActive(false);
        }

        void Update()
        {
            distanceText.text = $"{player.Meters:0.0} m";
        }

        void OnEnable()
        {
            player.CarController.OnCarFuelChange += Player_OnFuelChange;
            player.OnPlayerEnergyChange += Player_OnEnergyChange;
        }

        void OnDisable()
        {
            player.CarController.OnCarFuelChange -= Player_OnFuelChange;
            player.OnPlayerEnergyChange -= Player_OnEnergyChange;
        }

        private void Player_OnFuelChange(float fuel, float fuelMax)
        {
            float value = fuel / fuelMax;

            fuelSlider.value = value;
            fuelFill.color = Color.Lerp(Color.red, Color.green, value);
        }

        private void Player_OnEnergyChange(float energy, float energyMax)
        {
            float value = energy / energyMax;

            energySlider.value = value;
            energyFill.color = Color.Lerp(Color.red, Color.green, value);
        }
    }
}
