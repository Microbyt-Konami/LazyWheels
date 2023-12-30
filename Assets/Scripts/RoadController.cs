using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    public class RoadController : MonoBehaviour
    {
        // Components
        private SpriteRenderer rb;

        // Variables
        private float height;

        public float Height => height;

        private void Awake()
        {
            rb = GetComponent<SpriteRenderer>();
            height = rb.bounds.size.y;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
