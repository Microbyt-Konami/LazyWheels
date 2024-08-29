using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    public class RayTrigger : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Transform myTransform = null;
        [SerializeField] private Transform raysTransform;
        [SerializeField] private BoxCollider2D boxCollider2D;

        [field: SerializeField, Tooltip("Distances Car Cast Right (<0 no cast)"), Header("Result")] public float DistanceCollision { get; private set; } = -1f;

        //Debug
        [Header("Debug")]
        [SerializeField] private Color boxCollideColor;
        [SerializeField] private GameObject goCollision;
        [SerializeField] private Vector2 positionCollision;


        //Components
        [Header("Components")]
        [SerializeField] private Vector2 direction;
        [SerializeField] private float midSize;
        [SerializeField] private float size;

        public RaycastHit2D BoxCast(LayerMask lineLayerMask)
        {
            Vector2 origin = (Vector2)myTransform.position - direction * (midSize - 0.5f);
            RaycastHit2D hit = Physics2D.BoxCast(origin, Vector2.one, 0, direction, size, lineLayerMask);

            return hit;
        }

        void OnEnable()
        {
            DistanceCollision = -1f;
        }

        void Awake()
        {
            myTransform ??= GetComponent<Transform>();
            raysTransform = myTransform.parent;
            boxCollider2D ??= GetComponent<BoxCollider2D>();
            CalcDirection();
            boxCollider2D.enabled = false;
        }

        void FixedUpdate()
        {
            RaycastHit2D hit = BoxCast(boxCollider2D.includeLayers);

            if (hit.collider is not null)
            {
                GameObject go = myTransform.parent.transform.parent.gameObject;

                goCollision = hit.collider.gameObject;
                positionCollision = hit.point;
                this.DistanceCollision = hit.distance;
                Debug.Log($"{go.name} Collision: {goCollision.name} {positionCollision} {DistanceCollision}", goCollision);
                //Debug.Break();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = boxCollideColor;
            if (myTransform is not null && boxCollider2D is not null)
            {
                Vector2 position2 = (Vector2)myTransform.position;
                Gizmos.DrawWireCube(position2 + boxCollider2D.offset, boxCollider2D.size);
                Gizmos.DrawWireSphere(position2 - direction * midSize, midSize / 8);
            }
            if (DistanceCollision >= 0)
                Gizmos.DrawSphere(positionCollision, 1);
        }

        void OnValidate()
        {
            CalcDirection();
        }

        void CalcDirection()
        {
            if (boxCollider2D is null || raysTransform is null)
                return;

            //Vertical
            if (boxCollider2D.size.x < boxCollider2D.size.y)
            {
                direction = raysTransform.position.y < myTransform.position.y ? Vector2.up : Vector2.down;
                midSize = boxCollider2D.size.y / 2;
            }
            else // Horizontal
            {
                direction = raysTransform.position.x < myTransform.position.x ? Vector2.right : Vector2.left;
                midSize = boxCollider2D.size.x / 2;
            }
            size = midSize * 2;
        }
    }
}
