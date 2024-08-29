using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    [RequireComponent(typeof(RayTrigger))]
    public class LineTrigger : MonoBehaviour
    {
        private RayTrigger rayTrigger;

        [Header("Settings")]
        [SerializeField] private bool isRight;
        [SerializeField] private LayerMask layerMask;
        [field: SerializeField, Header("Result")] public float MetersLine { get; private set; } = -1f;

        void Awake()
        {
            rayTrigger = GetComponent<RayTrigger>();
        }

        void FixedUpdate()
        {
            RaycastHit2D hit = rayTrigger.BoxCast(layerMask);

            if (hit.collider is not null)
            {
                var line = hit.collider.gameObject.GetComponent<LineController>();

                if (line is not null)
                    MetersLine = isRight ? line.CalcMetersLinesRight(hit.point) : line.CalcMetersLinesLeft(hit.point);
            }
            else
                MetersLine = -1f;
        }
    }
}
