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
        [SerializeField] private LayerMask layerLineRays;

        [SerializeField, Tooltip("Distances Car Cast Up (<0 no cast)")] private float distCarCastUp = -1;
        [SerializeField, Tooltip("Distances Car Cast Down (<0 no cast)")] private float distCarCastDown = -1;
        [SerializeField, Tooltip("Distances Car Cast Left (<0 no cast)")] private float distCarCastLeft = -1;
        [SerializeField, Tooltip("Distances Car Cast Right (<0 no cast)")] private float distCarCastRight = -1;
        [SerializeField, Tooltip("Distances Car Cast Left (<0 no cast)")] private float distLineLeft = -1;
        [SerializeField, Tooltip("Distances Car Cast Right (<0 no cast)")] private float distLineRight = -1;

        // Component
        private Transform myTransform;

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
            distCarCastUp = RayCast(rayUpPoint, capsuleColliderUp, layerMaskRays);
            distCarCastDown = RayCast(rayDownPoint, capsuleColliderDown, layerMaskRays);
            distCarCastLeft = RayCast(rayLeftPoint, capsuleColliderLeft, layerMaskRays);
            distCarCastRight = RayCast(rayRightPoint, capsuleColliderRight, layerMaskRays);
            distLineLeft = RayCast(rayLeftPoint, capsuleColliderLeft, layerLineRays);
            distLineRight = RayCast(rayRightPoint, capsuleColliderRight, layerLineRays);

            float RayCast(Transform transform, CapsuleCollider2D collider, LayerMask layerMask)
            {
                // var positionGO = myTransform.position;
                // var offset = collider.offset;
                // var position = new Vector2(positionGO.x + offset.x, positionGO.y + offset.y);
                var position = transform.position;
                var colliderCast = Physics2D.OverlapCapsule(position, collider.size, collider.direction, 0, layerMask);
                var distance = colliderCast != null ? Vector3.Distance(position, colliderCast.transform.position) : -1;

                return distance;
            }
        }

        private void Awake()
        {
            myTransform = GetComponent<Transform>();
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

        private void FixedUpdate()
        {
            OnRaysCast();
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

        // Update is called once per frame
        //void Update()
        //{

        //}
    }
}
