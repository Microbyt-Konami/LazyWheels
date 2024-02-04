using MicrobytKonami.LazyWheels.Controllers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    public class Rays : MonoBehaviour
    {
        public const int countObjectsRay = 10;
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
        [SerializeField] private GameObject player;
        [SerializeField] private Vector2 positionCollideRayDown;
        [SerializeField] private int countObjectsRayDomn;
        [SerializeField] private Collider2D[] collidersRayDown = new Collider2D[countObjectsRay];
        [SerializeField] private GameObject[] rayDown = new GameObject[countObjectsRay];

        // Variables
        [Header("Variables"), SerializeField] private int myCarID;

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
        //void Start()
        //{
        //}

        private void FixedUpdate()
        {
            //int count;

            player = Physics2D.OverlapCapsule(myTransform.position, capsuleCollider.size, capsuleCollider.direction, 0, layerMask)?.gameObject;
            //count = Physics2D.OverlapCapsuleNonAlloc(rayDownPoint.position, capsuleColliderDown.size, capsuleColliderDown.direction, 0, collidersRayDown, layerMaskRays);
            //countObjectsRayDomn =
            countObjectsRayDomn = RayCast(rayDownPoint, capsuleColliderDown, ref positionCollideRayDown, rayDown, collidersRayDown);
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


        private int UpdateObjectsRay(GameObject[] objects, Collider2D[] colliders, int count)
        {
            int countObjects = 0;

            for (int i = 0; i < count; i++)
            {
                GameObject obj = colliders[i].gameObject;

                if (obj.GetInstanceID() != myCarID)
                    objects[countObjects++] = obj;
            }

            return countObjects;
        }

        private int RayCast(Transform transform, CapsuleCollider2D capsule, ref Vector2 positionCollider, GameObject[] objects, Collider2D[] colliders)
        {
            positionCollider.x = transform.position.x + capsuleCollider.offset.x;
            positionCollider.y = transform.position.y + capsuleCollider.offset.y;

            int count = Physics2D.OverlapCapsuleNonAlloc(positionCollider, capsule.size, capsule.direction, 0, colliders, layerMaskRays);
            int countNew = UpdateObjectsRay(objects, colliders, count);

            return countNew;
        }
    }
}
