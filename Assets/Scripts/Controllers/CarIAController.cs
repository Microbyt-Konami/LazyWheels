using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels.Controllers
{
    [RequireComponent(typeof(CarController))]
    public class CarIAController : MonoBehaviour
    {
        private CarController carController;

        private void Awake()
        {
            carController = GetComponent<CarController>();
        }

        // Start is called before the first frame update
        void Start()
        {
            carController.Mover(0);
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}