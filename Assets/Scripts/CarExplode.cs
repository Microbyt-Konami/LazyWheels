using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MicrobytKonami.LazyWheels
{
    public class CarExplode : MonoBehaviour
    {
        private ParticleSystem _particleSystem;
        private AudioSource audioSource;

        public UnityEvent OnExplodeEnd;

        public float Duration => _particleSystem.main.duration;

        void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            _particleSystem.Play();
            audioSource.Play();
        }

        void OnParticleSystemStopped()
        {
            Debug.Log("OnParticleSystemStopped");
            OnExplodeEnd.Invoke();
        }
    }
}
