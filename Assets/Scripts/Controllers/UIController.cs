using MicrobytKonami.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    public class UIController : MonoBehaviourSingleton<UIController>
    {
        [SerializeField] private GameObject panelGameOver;

        public void ShowGameOver()
        {
            panelGameOver.SetActive(true);
        }

        // Start is called before the first frame update
        void Start()
        {
            panelGameOver.SetActive(false);
        }
    }
}
