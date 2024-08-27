using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class RayTrigger : MonoBehaviour
    {
        //Debug
        [Header("Debug")]
        [SerializeField] private Color boxCollideColor;
        [SerializeField] private GameObject goCollision;
        [SerializeField] private Vector2 positionCollision;
        [field: SerializeField, Tooltip("Distances Car Cast Right (<0 no cast)")] public float distanceCollision { get; private set; } = -1f;

        //Components
        [Header("Components")]
        [SerializeField] private Transform myTransform;
        [SerializeField] private BoxCollider2D boxCollider2D;
        [SerializeField] private Vector2 direction1;
        [SerializeField] private Vector2 direction2;

        void OnEnable()
        {
            distanceCollision = -1f;
        }

        void Awake()
        {
            myTransform ??= GetComponent<Transform>();
            boxCollider2D ??= GetComponent<BoxCollider2D>();
            boxCollider2D.enabled = false;
            direction1 = boxCollider2D.size.x == 1 ? Vector2.up : Vector2.right;
            direction2 = boxCollider2D.size.x == 1 ? Vector2.down : Vector2.left;
        }

        void FixedUpdate()
        {
            Vector2 position = (Vector2)myTransform.position + boxCollider2D.offset;
            Collider2D colliderCollision = Physics2D.OverlapBox(position, boxCollider2D.size, 0, boxCollider2D.includeLayers);

            if (colliderCollision is not null)
            {
                Bounds collisionBounds = colliderCollision.bounds;
                Bounds playerBounds = boxCollider2D.bounds;
                float x = Physics2DPhysics2DEx.CalculatePointCollisionXBetween2Bounds(collisionBounds, playerBounds);
                float y = Physics2DPhysics2DEx.CalculatePointCollisionYBetween2Bounds(collisionBounds, playerBounds);
                Vector2 positionCollision = new Vector2(x, y);
                float distanceCollision = Vector2.Distance(myTransform.position, positionCollision);

                if (this.distanceCollision < 0 || this.distanceCollision > distanceCollision)
                {
                    goCollision = colliderCollision.gameObject;
                    this.positionCollision = positionCollision;
                    this.distanceCollision = distanceCollision;
                    Debug.Log($"Collision: {goCollision.name} {positionCollision} {distanceCollision}", goCollision);
                    //Debug.Break();
                }
            }
        }

        // void OnTriggerEnter2D(Collider2D other)
        // {
        //     Bounds collisionBounds = other.bounds;
        //     Bounds playerBounds = boxCollider2D.bounds;
        //     float x = Physics2DPhysics2DEx.CalculatePointCollisionXBetween2Bounds(collisionBounds, playerBounds);
        //     float y = Physics2DPhysics2DEx.CalculatePointCollisionYBetween2Bounds(collisionBounds, playerBounds);
        //     Vector2 positionCollision = new Vector2(x, y);
        //     float distanceCollision = Vector2.Distance(myTransform.position, positionCollision);

        //     if (this.distanceCollision < 0 || this.distanceCollision > distanceCollision)
        //     {
        //         goCollision = other.gameObject.transform.parent.gameObject;
        //         this.positionCollision = positionCollision;
        //         this.distanceCollision = distanceCollision;
        //         Debug.Log($"Collision: {goCollision.name} {positionCollision} {distanceCollision}", goCollision);
        //         Debug.Break();
        //     }
        // }

        private void OnDrawGizmos()
        {
            Gizmos.color = boxCollideColor;
            if (myTransform is not null && boxCollider2D is not null)
                Gizmos.DrawWireCube((Vector2)myTransform.position + boxCollider2D.offset, boxCollider2D.size);
            if (distanceCollision >= 0)
                Gizmos.DrawSphere(positionCollision, 1);
        }
    }
}
