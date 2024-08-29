using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEngine
{
    public static class GizmosEx
    {
        // https://discussions.unity.com/t/drawing-a-2d-capsule-same-of-capsulecollider2d/227921/3
        public static void DrawCapsule2D(Transform transform, Vector3 offset, Vector2 size, CapsuleDirection2D direction)
        {
            float step = 0.2f;
            float r = Mathf.Min(size.x, size.y) / 2;
            float d = Mathf.Max(size.x, size.y) / 2 - r;
            Vector3 lp = Vector3.positiveInfinity;
            for (float a = -Mathf.PI; a <= Mathf.PI + step; a += step)
            {
                float x = Mathf.Cos(a);
                float y = Mathf.Sin(a);
                Vector3 np = offset + new Vector3(x, y) * r;
                np += (direction == CapsuleDirection2D.Vertical ? Vector3.up : Vector3.zero) * d * Mathf.Sign(y);
                np += (direction == CapsuleDirection2D.Horizontal ? Vector3.right : Vector3.zero) * d * Mathf.Sign(x);
                np = transform.TransformPoint(np);
                if (lp != Vector3.positiveInfinity) Gizmos.DrawLine(lp, np);
                lp = np;
            }
        }

        public static void DrawCapsule2D(Transform transform, CapsuleCollider2D capsuleCollider) => DrawCapsule2D(transform, capsuleCollider.offset, capsuleCollider.size, capsuleCollider.direction);

        public static void DrawCapsule2D(Transform transform, CapsuleCollider2D capsuleCollider, Color color)
        {
            Gizmos.color = color;
            DrawCapsule2D(transform, capsuleCollider);
        }

        // https://forum.unity.com/threads/drawing-capsule-gizmo.354634/
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
    }
}
