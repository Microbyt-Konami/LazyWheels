using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class LineController : MonoBehaviour
    {
        // Fields
        [SerializeField] private LineController lineRight;
        [SerializeField] private LineController lineLeft;

        private BoxCollider2D boxCollider2D;

        void Awake()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
        }

        public float CalcMetersRight()
        {
            var line = this;
            var meters = 0f;

            do
            {
                meters += boxCollider2D.size.x;
                line = line.lineRight;
            } while (line is not null);

            return meters;
        }

        public float CalcMetersLeft()
        {
            var line = this;
            var meters = 0f;

            do
            {
                meters += boxCollider2D.size.x;
                line = line.lineLeft;
            } while (line is not null);

            return meters;
        }

        public float CalcMetersLinesRight(Vector2 point)
        {
            var bounds = boxCollider2D.bounds;
            var meters = 0f;

            if (point.x >= bounds.min.x && point.x <= bounds.max.x)
            {
                meters += bounds.max.x - point.x;
                if (lineRight is not null)
                    meters += lineRight.CalcMetersRight();
            }

            return meters;
        }

        public float CalcMetersLinesLeft(Vector2 point)
        {
            var bounds = boxCollider2D.bounds;
            var meters = 0f;

            if (point.x >= bounds.min.x && point.x <= bounds.max.x)
            {
                meters += point.x - bounds.min.x;
                if (lineLeft is not null)
                    meters += lineLeft.CalcMetersLeft();
            }

            return meters;
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
