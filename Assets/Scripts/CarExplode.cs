using System.Collections;
using System.Collections.Generic;
using MicrobytKonami.LazyWheels.Controllers;
using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    public class CarExplode : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        public void Explode(CarController carIA)
        {
            carIA.StartCarFade(_particleSystem.main.duration);
            _particleSystem.Play(false);
        }

        void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        IEnumerator FinishExplode(float time, PlayerController player)
        {
            yield return new WaitForSeconds(time);

            player.Die();
        }
    }
}
