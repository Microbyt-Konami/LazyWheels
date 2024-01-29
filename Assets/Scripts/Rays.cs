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
            float w = ray.x / 2;
            float h = ray.y / 2;

            Gizmos.color = Color.yellow;
            // https://discussions.unity.com/t/drawing-a-2d-capsule-same-of-capsulecollider2d/227921/3
            // https://forum.unity.com/threads/drawing-capsule-gizmo.354634/
            //Gizmos.DrawWireSphere(rayUpDownPoint.position + h * Vector3.up, h);
            //Gizmos.DrawWireMesh
            // collider is CapsuleCollider
            //Gizmos.matrix = collider.transform.localToWorldMatrix;

            //Vector3 offset = Vector3.zero;
            //offset[collider.direction] = collider.height * 0.5f - collider.radius;
            //DrawWireCapsule(collider.center + offset, collider.center - offset, collider.radius);

        }

        public static void DrawWireCapsule(Vector3 p1, Vector3 p2, float radius)
        {
#if UNITY_EDITOR
            // Special case when both points are in the same position
            if (p1 == p2)
            {
                // DrawWireSphere works only in gizmo methods
                Gizmos.DrawWireSphere(p1, radius);
                return;
            }
            using (new UnityEditor.Handles.DrawingScope(Gizmos.color, Gizmos.matrix))
            {
                Quaternion p1Rotation = Quaternion.LookRotation(p1 - p2);
                Quaternion p2Rotation = Quaternion.LookRotation(p2 - p1);
                // Check if capsule direction is collinear to Vector.up
                float c = Vector3.Dot((p1 - p2).normalized, Vector3.up);
                if (c == 1f || c == -1f)
                {
                    // Fix rotation
                    p2Rotation = Quaternion.Euler(p2Rotation.eulerAngles.x, p2Rotation.eulerAngles.y + 180f, p2Rotation.eulerAngles.z);
                }
                // First side
                UnityEditor.Handles.DrawWireArc(p1, p1Rotation * Vector3.left, p1Rotation * Vector3.down, 180f, radius);
                UnityEditor.Handles.DrawWireArc(p1, p1Rotation * Vector3.up, p1Rotation * Vector3.left, 180f, radius);
                UnityEditor.Handles.DrawWireDisc(p1, (p2 - p1).normalized, radius);
                // Second side
                UnityEditor.Handles.DrawWireArc(p2, p2Rotation * Vector3.left, p2Rotation * Vector3.down, 180f, radius);
                UnityEditor.Handles.DrawWireArc(p2, p2Rotation * Vector3.up, p2Rotation * Vector3.left, 180f, radius);
                UnityEditor.Handles.DrawWireDisc(p2, (p1 - p2).normalized, radius);
                // Lines
                UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.down * radius, p2 + p2Rotation * Vector3.down * radius);
                UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.left * radius, p2 + p2Rotation * Vector3.right * radius);
                UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.up * radius, p2 + p2Rotation * Vector3.up * radius);
                UnityEditor.Handles.DrawLine(p1 + p1Rotation * Vector3.right * radius, p2 + p2Rotation * Vector3.left * radius);
            }
#endif
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
