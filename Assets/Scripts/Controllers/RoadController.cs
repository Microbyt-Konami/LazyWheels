using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels.Controllers
{
    public class RoadController : MonoBehaviour
    {
        // Variables
        private float height;

        public float Height => height;

        public void CalcHeight()
        {
            var rb = GetComponent<SpriteRenderer>();

            height = rb.bounds.size.y;
        }

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
