using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MicrobytKonami.LazyWheels
{
    public class UIInfo : MonoBehaviour
    {
        [Header("UI")]
        [SerializeField] private Image fuelFill;
        [SerializeField] private Slider fuelSlider;
        [SerializeField] private Image energyFill;
        [SerializeField] private Slider energySlider;

        public void SetFuel(float fuel, float fuelMax)
        {
            float value = fuel / fuelMax;

            fuelSlider.value = value;
            fuelFill.color = Color.Lerp(Color.red, Color.green, value);
        }

        public void SetEnergy(float energy, float energyMax)
        {
            float value = energy / energyMax;

            energySlider.value = value;
            energyFill.color = Color.Lerp(Color.red, Color.green, value);
        }
    }
}
