using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MicrobytKonami.LazyWheels.UI
{
    public class UICounterInitial : MonoBehaviour
    {
        [SerializeField] private Image imageCounter;
        [SerializeField] private Sprite[] spritesCounters;
        [SerializeField] private AudioSource cuentaRegresivaSound;

        public Coroutine StartCounter()
        {
            return StartCoroutine(StartCounterCoroutine());
        }

        public IEnumerator StartCounterCoroutine()
        {
            yield return new WaitForSeconds(2);

            imageCounter.sprite = spritesCounters[1];
            cuentaRegresivaSound.Play();
            yield return new WaitForSeconds(1);

            imageCounter.sprite = spritesCounters[2];
            cuentaRegresivaSound.Play();
            yield return new WaitForSeconds(1);

            imageCounter.sprite = spritesCounters[3];
        }
    }
}
