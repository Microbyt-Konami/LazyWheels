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
        private float yTop;

        public float Height => height;
        public float YTop => yTop;

        public void CalcHeight()
        {
            transformBlock = GetComponent<Transform>();

            var roads = GetComponentsInChildren<RoadController>();
            float _height = 0, _yTop = transformBlock.position.y;

            foreach (var road in roads)
            {
                _height += road.Height;
                if (road.GetComponent<Transform>().position.y > 0)
                    _yTop += road.Height;
                else
                    _yTop += road.Height / 2f;
            }

            height = _height;
            yTop = _yTop;
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
