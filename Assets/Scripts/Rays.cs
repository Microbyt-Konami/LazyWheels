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
        [SerializeField] private Collider2D collider2dTmp;

        // Variables
        [Header("Variables"), SerializeField] private int myCarID;

        public void OnRaysCast()
        {
            distCarCastUp = RayCast(new Vector3(rayUpPoint.position.x, rayUpPoint.position.y + capsuleColliderUp.size.y / 2, rayUpPoint.position.z), capsuleColliderUp, layerMaskRays);
            distCarCastDown = RayCast(new Vector3(rayDownPoint.position.x, rayDownPoint.position.y - capsuleColliderDown.size.y / 2, rayDownPoint.position.z), capsuleColliderDown, layerMaskRays);
            distCarCastLeft = RayCast(rayLeftPoint.position, capsuleColliderLeft, layerMaskRays);
            distCarCastRight = RayCast(rayRightPoint.position, capsuleColliderRight, layerMaskRays);
            distLineLeft = RayCast(rayLeftPoint.position, capsuleColliderLeft, layerLineRays);
            distLineRight = RayCast(rayRightPoint.position, capsuleColliderRight, layerLineRays);

            float RayCast(Vector3 position, CapsuleCollider2D collider, LayerMask layerMask)
            {
                // var positionGO = myTransform.position;
                // var offset = collider.offset;
                // var position = new Vector2(positionGO.x + offset.x, positionGO.y + offset.y);
                // bug en la dirección en el scripts de Rays, método OnRaysCast
                //var position = transform.position;
                var colliderCast = Physics2D.OverlapCapsule(position, collider.size / 2, collider.direction, 0, layerMask);
                var distance = colliderCast != null ? Vector3.Distance(position, colliderCast.transform.position) : -1;

                if (distance > 0)
                    collider2dTmp = colliderCast;

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

            Gizmos.color = Color.red;
            if (rayUpPoint != null && capsuleColliderUp != null)
            {
                Gizmos.DrawSphere(new Vector3(rayUpPoint.position.x, rayUpPoint.position.y + capsuleColliderUp.size.y / 2, rayUpPoint.position.z), capsuleColliderUp.size.x / 4);
                GizmosEx.DrawCapsule2D(rayUpPoint, capsuleColliderUp);
            }

            Gizmos.color = Color.gray;
            if (rayDownPoint != null && capsuleColliderDown != null)
            {
                Gizmos.DrawSphere(new Vector3(rayDownPoint.position.x, rayDownPoint.position.y - capsuleColliderDown.size.y / 2, rayDownPoint.position.z), capsuleColliderDown.size.x / 4);
                GizmosEx.DrawCapsule2D(rayDownPoint, capsuleColliderDown);
            }

            Gizmos.color = Color.green;
            if (rayLeftPoint != null && capsuleColliderLeft != null)
            {
                Gizmos.DrawSphere(rayLeftPoint.position, capsuleColliderLeft.size.y / 4);
                GizmosEx.DrawCapsule2D(rayLeftPoint, capsuleColliderLeft);
            }

            Gizmos.color = Color.yellow;
            if (rayRightPoint != null && capsuleColliderRight != null)
            {
                Gizmos.DrawSphere(rayRightPoint.position, capsuleColliderRight.size.y / 4);
                GizmosEx.DrawCapsule2D(rayRightPoint, capsuleColliderRight);
            }
        }

        // Update is called once per frame
        //void Update()
        //{

        //}
    }
}
