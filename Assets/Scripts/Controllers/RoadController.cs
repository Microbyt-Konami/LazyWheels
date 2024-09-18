using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels.Controllers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class RoadController : MonoBehaviour
    {
        private float height;

        public float Height => height;

        public void CalcHeight()
        {
            var rb = GetComponent<SpriteRenderer>();

            height = rb.bounds.size.y;
        }
    }
}
