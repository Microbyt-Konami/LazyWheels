using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MicrobytKonami.LazyWheels
{
    public class MainMenui : MonoBehaviour
    {
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject howToPlayPanel;

        public void Play() => StartCoroutine(PlayCoroutine());

        IEnumerator PlayCoroutine()
        {
            mainPanel.SetActive(false);
            howToPlayPanel.SetActive(true);

            yield return new WaitForSeconds(3);

            SceneManager.LoadScene("Game");
        }
    }
}
