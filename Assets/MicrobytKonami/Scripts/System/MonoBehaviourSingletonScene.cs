using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.System
{
    public class MonoBehaviourSingletonScene<T> : MonoBehaviour where T : Component
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    Debug.LogError($"{typeof(T).Name} is null");

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            _instance = this as T;
        }
    }
}
