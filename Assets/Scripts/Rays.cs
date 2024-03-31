using MicrobytKonami.LazyWheels.Controllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    public class Rays : MonoBehaviour
    {
        public const int countMaxObjectsRay = 10;
        //Fields
        [SerializeField] private Transform rayUpPoint;
        [SerializeField] private Transform rayDownPoint;
        [SerializeField] private Transform rayRightPoint;
        [SerializeField] private Transform rayLeftPoint;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private LayerMask layerMaskRays;

        // Component
        private Transform myTransform;
        private CarController carController;
        private CarIAController carIAController;
        [Header("Components"), SerializeField] private GameObject myCar;
        [SerializeField] private CapsuleCollider2D capsuleCollider;
        [SerializeField] private CapsuleCollider2D capsuleColliderUp;
        [SerializeField] private CapsuleCollider2D capsuleColliderDown;
        [SerializeField] private CapsuleCollider2D capsuleColliderRight;
        [SerializeField] private CapsuleCollider2D capsuleColliderLeft;

        // Variables
        [Header("Variables"), SerializeField] private int myCarID;

        public void OnRaysCast()
        {
            carIAController.DistCarCastUp = RayCast(capsuleColliderUp);
            carIAController.DistCarCastDown = RayCast(capsuleColliderDown);
            carIAController.DistCarCastLeft = RayCast(capsuleColliderLeft);
            carIAController.DistCarCastRight = RayCast(capsuleColliderRight);

            float RayCast(CapsuleCollider2D collider)
            {
                var positionGO = myTransform.position;
                var offset = collider.offset;
                var position = new Vector2(positionGO.x + offset.x, positionGO.y + offset.y);
                var colliderCast = Physics2D.OverlapCapsule(position, collider.size, collider.direction, 0, layerMaskRays);

                return colliderCast != null ? Vector3.Distance(positionGO, colliderCast.transform.position) : -1;
            }
        }

        private void Awake()
        {
            myTransform = GetComponent<Transform>();
            carController = GetComponentInParent<CarController>();
            carIAController = GetComponentInParent<CarIAController>();
            myCar = carIAController.MyCar;
            myCarID = myCar.GetInstanceID();
            capsuleCollider = GetComponent<CapsuleCollider2D>();
            capsuleColliderUp = rayUpPoint.GetComponent<CapsuleCollider2D>();
            capsuleColliderDown = rayDownPoint.GetComponent<CapsuleCollider2D>();
            capsuleColliderRight = rayRightPoint.GetComponent<CapsuleCollider2D>();
            capsuleColliderLeft = rayLeftPoint.GetComponent<CapsuleCollider2D>();
            capsuleCollider.enabled = false;
            capsuleColliderUp.enabled = false;
            capsuleColliderDown.enabled = false;
            capsuleColliderLeft.enabled = false;
            capsuleColliderRight.enabled = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            RaysController.Instance.AddRay(this);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            if (myTransform != null && capsuleCollider != null)
                GizmosEx.DrawCapsule2D(myTransform, capsuleCollider);
            if (rayUpPoint != null && capsuleColliderUp != null)
                GizmosEx.DrawCapsule2D(rayUpPoint, capsuleColliderUp);
            if (rayDownPoint != null && capsuleColliderDown != null)
                GizmosEx.DrawCapsule2D(rayDownPoint, capsuleColliderDown);
            if (rayLeftPoint != null && capsuleColliderLeft != null)
                GizmosEx.DrawCapsule2D(rayLeftPoint, capsuleColliderLeft);
            if (rayRightPoint != null && capsuleColliderRight != null)
                GizmosEx.DrawCapsule2D(rayRightPoint, capsuleColliderRight);
        }

        private void OnDestroy() => RaysController.Instance.RemoveRay(this);

        // Update is called once per frame
        //void Update()
        //{

        //}
    }
}
