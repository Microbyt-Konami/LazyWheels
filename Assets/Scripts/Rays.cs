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
        [SerializeField] private Transform rayDownPoint;
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
            float w = ray.x / 2;
            float h = ray.y / 2;

            //print($"ray : {ray.x} {ray.y}");
            //print($"DrawCapsule2D({rayUpPoint}, {new Vector3(0, -h / 2f)}, {ray}");
            //Gizmos.color = Color.yellow;
            //GizmosEx.DrawCapsule2D(rayUpPoint, new Vector3(0, -h), ray, CapsuleDirection2D.Horizontal);
            //Gizmos.color = Color.red;
            //GizmosEx.DrawCapsule2D(rayDownPoint, new Vector3(0, h), ray, CapsuleDirection2D.Horizontal);
            Gizmos.color = Color.yellow;
            GizmosEx.DrawCapsule2D(rayUpPoint, new Vector3(0, 0), ray, CapsuleDirection2D.Horizontal);
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
