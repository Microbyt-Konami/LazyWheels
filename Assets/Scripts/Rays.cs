using MicrobytKonami.LazyWheels.Controllers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    public class Rays : MonoBehaviour
    {
        //Fields
        [SerializeField] private Transform rayUpPoint;
        [SerializeField] private Transform rayUpDownPoint;
        [SerializeField] private Transform rayRightPoint;
        [SerializeField] private Transform rayLeftPoint;
        [SerializeField] private float timeRay = 3;

        // Component
        private CarController carController;

        [ExecuteInEditMode]
        private void Awake()
        {
            carController = GetComponentInParent<CarController>();
        }

        private void FixedUpdate()
        {
            //Physics2D.OverlapCapsule()            
        }

        private void OnDrawGizmos()
        {
            if (carController == null)
                return;

            Vector2 ray = carController.GetRay(timeRay);
            float h = ray.y / 2;

            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(rayUpDownPoint.position + h * Vector3.up, h);
        }

        // Start is called before the first frame update
        //void Start()
        //{
        //}

        // Update is called once per frame
        //void Update()
        //{

        //}
    }
}
