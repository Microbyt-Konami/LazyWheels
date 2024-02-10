using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MicrobytKonami.System;
using MicrobytKonami.LazyWheels.Controllers;

namespace MicrobytKonami.LazyWheels
{
    public class RaysController : MonoBehaviourSingleton<RaysController>
    {
        public const int countMaxRays = 40;
        [SerializeField] private LimitedList<Rays> rays;

        public void AddRay(Rays ray) => rays.Add(ray);

        // Start is called before the first frame update
        void Start()
        {
            rays = new LimitedList<Rays>(countMaxRays);
        }

        // Update is called once per frame
        //void Update()
        //{

        //}

        private void FixedUpdate()
        {
            foreach (var ray in rays)
                ray.OnRays();
        }
    }
}
