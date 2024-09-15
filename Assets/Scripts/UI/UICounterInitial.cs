using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace MicrobytKonami.LazyWheels
{
    public class UICounterInitial : MonoBehaviour
    {
        [SerializeField] private Image imageCounter;
        [SerializeField] private Sprite[] spritesCounters;
        [SerializeField] private AudioSource cuentaRegresivaSound;
        [SerializeField] private AudioSource inicioFXSound;
        [SerializeField] float time = 3;

        public Coroutine StartCounter()
        {
            return StartCoroutine(StartCounterCoroutine());
        }

        public IEnumerator StartCounterCoroutine()
        {
            float t = time / spritesCounters.Length;

            foreach (var sprite in spritesCounters)
            {
                imageCounter.sprite = sprite;
                cuentaRegresivaSound.Play();

                yield return new WaitForSeconds(t);
            }

            inicioFXSound.Play();
        }
    }
}
