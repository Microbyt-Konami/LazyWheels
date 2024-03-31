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
        public void RemoveRay(Rays ray) => rays.Remove(ray);

        protected override void Awake()
        {
            rays = new LimitedList<Rays>(countMaxRays);
        }

        // Start is called before the first frame update
        //void Start()
        //{
        //}

        // Update is called once per frame
        //void Update()
        //{

        //}

        private void FixedUpdate()
        {
            for (var i = 0; i < rays.Count; i++)
            {
                Rays ray = rays[i];

                if (ray.enabled)
                    ray.OnRaysCast();
            }
        }
    }
}
