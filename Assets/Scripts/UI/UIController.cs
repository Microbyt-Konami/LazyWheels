using MicrobytKonami.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

using TMPro;

using MicrobytKonami.LazyWheels.Controllers;

namespace MicrobytKonami.LazyWheels.UI
{
    public class UIController : MonoBehaviourSingletonScene<UIController>
    {
        [Header("References")]
        [SerializeField] private PlayerController player;
        [field: SerializeField] public UIInfo Info { get; private set; }
        [field: SerializeField] public UICounterInitial CounterInitial { get; private set; }

        [Header("UI")]
        [SerializeField] private GameObject panelGameOver;
        [SerializeField] private TextMeshProUGUI distanceText;

        // Components

        public void ShowGameOver()
        {
            panelGameOver.SetActive(true);
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
    }
}
