using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class RayTrigger : MonoBehaviour
    {
        private BoxCollider2D boxCollider2D;

        [field: SerializeField, Header("Debug")] public float distance { get; private set; }

        void OnEnable()
        {
            distance = -1f;
        }

        void Awake()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            Bounds collisionBounds = other.bounds;
            Bounds playerBounds = boxCollider2D.bounds;
            float x = Physics2DPhysics2DEx.CalculatePointCollisionXBetween2Bounds(collisionBounds, playerBounds);
            float y = Physics2DPhysics2DEx.CalculatePointCollisionYBetween2Bounds(collisionBounds, playerBounds);
            Vector2 positionColision = new Vector2(x, y);
            float distance = Vector2.Distance(transform.position, positionColision);

            if (this.distance > distance)
                this.distance = distance;
        }
    }
}
