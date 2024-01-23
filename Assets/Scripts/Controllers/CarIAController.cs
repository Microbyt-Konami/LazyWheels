using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels.Controllers
{
    [RequireComponent(typeof(CarController))]
    public class CarIAController : MonoBehaviour
    {
        [SerializeField] private bool canChangeLane;
        private CarController carController;

        public bool IsMoving
        {
            get => carController.IsMoving;
            set => carController.IsMoving = value;
        }

        public void SetParent(Transform parent) => carController.SetParent(parent);

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
            if (IsMoving)
            {
                Move();
            }
        }

        private void Move()
        {
            if (canChangeLane)
            {
                ChangeLineForNoCrash();
            }
        }

        private void ChangeLineForNoCrash()
        {

        }
    }
}