using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MicrobytKonami.LazyWheels
{
    public class CarExplode : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        public UnityEvent OnExplodeEnd;

        public float Duration => _particleSystem.main.duration;

        void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        void OnParticleSystemStopped()
        {
            Debug.Log("OnParticleSystemStopped");
            OnExplodeEnd.Invoke();
        }
    }
}
