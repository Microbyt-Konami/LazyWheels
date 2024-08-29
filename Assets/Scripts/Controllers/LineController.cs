using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class LineController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool isRight;
        [SerializeField] private LineController lineRight;
        [SerializeField] private LineController lineLeft;

        private BoxCollider2D boxCollider2D;

        public float MetersWith { get; private set; }

        public LineController GetLineLastRight()
        {
            var line = this;
            LineController lineRet;

            do
            {
                lineRet = line;
                line = line.lineRight;
            } while (line is not null);

            return line;
        }

        public LineController GetLineLastLeft()
        {
            var line = this;
            LineController lineRet;

            do
            {
                lineRet = line;
                line = line.lineLeft;
            } while (line is not null);

            return line;
        }

        public float Distance(float x)
        {
            var bounds = boxCollider2D.bounds;

            return (x >= bounds.min.x && x <= bounds.max.x) ? bounds.max.x - x : 0;
        }

        public float Center(float width) => (MetersWith - width) / 2f;

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

        void Awake()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            MetersWith = boxCollider2D.size.x;
        }
    }
}
