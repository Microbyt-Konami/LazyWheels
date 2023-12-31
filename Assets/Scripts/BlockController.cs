using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    public class BlockController : MonoBehaviour
    {
        // Components
        private Transform transformBlock;

        // Variables
        private float height;

        public float Height => height;

        public void CalcHeight()
        {
            transformBlock = GetComponent<Transform>();

            var roads = GetComponentsInChildren<RoadController>();
            var _height = 0f;

            foreach (var road in roads)
                _height += road.Height;

            height = _height;            
        }

        //private void Awake()
        //{

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
