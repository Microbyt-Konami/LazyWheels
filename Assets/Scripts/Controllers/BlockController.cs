using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels.Controllers
{
    public class BlockController : MonoBehaviour
    {
        // Fields
        [SerializeField] GameObject calles;

        // Components
        private Transform transformCarIAs;
        private CarIAController[] carsIAs;

        // Variables
        private float height;
        private float heightBlock0;
        private float heightFromBlock0;
        private float yTop;
        private float yBottom;

        public float Height => height;
        public float HeightBlock0 => heightBlock0;
        public float HeightFromBlock0 => heightFromBlock0;
        public float YTop => yTop;
        public float YBottom => yBottom;

        public void CalcHeight()
        {
            var transformBlock = GetComponent<Transform>();
            var transformCalles = calles.GetComponent<Transform>();

            float _height, _heightFromBlock0, _yTop, _yBottom;

            _height = _heightFromBlock0 = 0;
            _yTop = _yBottom = transformBlock.position.y;
            for (var i = 0; i < transformCalles.childCount; i++)
            {
                var road = transformCalles.GetChild(i).GetComponent<RoadController>();

                road.CalcHeight();
                _height += road.Height;
                if (i != 0)
                {
                    _yTop += road.Height;
                    _heightFromBlock0 += road.Height;
                }
                else
                {
                    heightBlock0 = road.Height / 2f;
                    _heightFromBlock0 += heightBlock0;
                    _yTop += heightBlock0;
                    _yBottom -= heightBlock0;
                }
            }

            height = _height;
            heightFromBlock0 = _heightFromBlock0;
            yTop = _yTop;
            yBottom = _yBottom;
        }

        //private void Awake()
        //{

        //}

        // Start is called before the first frame update
        void Start()
        {
            transformCarIAs = GameObject.Find("CarIAs").GetComponent<Transform>();
            LoadCarsIAs();
            DisableAllCarsIAs();
        }

        // Update is called once per frame
        void Update()
        {
//if (Camera.main.ray)
        }

        //private void OnBecameInvisible()
        //{
        //    print($"{name} invisible");
        //}

        void LoadCarsIAs() => carsIAs = transformCarIAs.GetComponents<CarIAController>();

        void DisableAllCarsIAs()
        {
            foreach (var carIA in carsIAs)
                carIA.IsMoving = false;
        }
    }
}