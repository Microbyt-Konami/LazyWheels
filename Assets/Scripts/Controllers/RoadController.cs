using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels.Controllers
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class RoadController : MonoBehaviour
    {
        //Fields

        // Variables
        private float height;
        //private LineController[] lines;

        public float Height => height;

        public void CalcHeight()
        {
            var rb = GetComponent<SpriteRenderer>();

            height = rb.bounds.size.y;
        }

        //private void Awake()
        //{
        //    lines = GetComponentsInChildren<LineController>();
        //}

        // Start is called before the first frame update
        //void Start()
        //{

        //}

        // Update is called once per frame
        //void Update()
        //{

        //}

        //private void OnBecameInvisible()
        //{
        //    print($"{name} invisible");
        //}
    }
}
