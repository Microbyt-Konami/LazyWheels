using MicrobytKonami.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MicrobytKonami.LazyWheels.UI
{
    public class UIController : MonoBehaviourSingleton<UIController>
    {
        [SerializeField] private GameObject panelGameOver;
        [SerializeField] private Image imageTest;
        [SerializeField] private Slider sliderTest;

        public void ShowGameOver()
        {
            panelGameOver.SetActive(true);
        }

        // Start is called before the first frame update
        void Start()
        {
            panelGameOver.SetActive(false);
        }

        //private void OnValidate()
        //{
        //    if (imageTest is not null && sliderTest is not null)
        //    {
        //        imageTest.color = Color.Lerp(Color.red, Color.green, sliderTest.value);
        //    }
        //}

        private void Update()
        {
            imageTest.color = Color.Lerp(Color.red, Color.green, sliderTest.value);
        }
    }
}
