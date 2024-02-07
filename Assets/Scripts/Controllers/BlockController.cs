using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using UnityEngine;

namespace MicrobytKonami.LazyWheels.Controllers
{
    public class BlockController : MonoBehaviour
    {
        // Fields
        [SerializeField] private GameObject calles;
        [SerializeField] private bool startMoveAllCarsIA;

        // Components
        private Transform myTransform;
        private Transform transformCarIAs;
        private List<CarIAController> carsIAs;
        private GameController gameController;

        // Variables
        private float height, heightBlock0, heightFromBlock0;
        [Header("Variables"), SerializeField] private float yTop;
        [SerializeField] private float yBottom;
        [SerializeField] private float yOld = float.MaxValue;

        public bool StartMoveAllCarsIA { get => startMoveAllCarsIA; set => startMoveAllCarsIA = value; }
        public float Height => height;
        public float HeightBlock0 => heightBlock0;
        public float HeightFromBlock0 => heightFromBlock0;
        public float YTop => yTop;
        public float YBottom => yBottom;
        public float YOld { get => yOld; set => yOld = value; }

        public List<CarIAController> CarsIAs => carsIAs;

        public void SetUp()
        {
            gameController = GameController.Instance;
            CalcHeight();
            LoadCarsIAs();
            MoveAllCarsIAs(startMoveAllCarsIA);
            StartCoroutine(DestroyBlockIfYOldCoroutine());
        }

        public void MoveCarIA(CarIAController carIA, bool move = true)
        {
            if (move == carIA.IsMoving)
                return;

            carIA.IsMoving = move;
            gameController.MoveCarIA(carIA);
        }

        public void MoveAllCarsIAs(bool move = true)
        {
            print($"StopAllCarsIAs {gameObject.name}");
            foreach (var carIA in carsIAs)
                MoveCarIA(carIA, move);
        }

        public void ShowAndMoveAllCarsIAs() => ShowAndMoveCarsIAs(carsIAs);

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

        private void ShowAndMoveCarsIAs(ICollection<CarIAController> carIAs)
        {
            if (carIAs != null)
            {
                foreach (var carIA in carIAs)
                {
                    carIA.IsMoving = true;
                    carIA.gameObject.SetActive(true);
                }
            }
        }
        public bool RemoveCarIA(CarIAController carIA) => carsIAs.Remove(carIA);
        public void AddCarIA(CarIAController carIA)
        {
            carIA.transform.parent = transformCarIAs;
            carsIAs.Add(carIA);
        }

        private void Awake()
        {
            myTransform = GetComponent<Transform>();
            transformCarIAs = myTransform.Find("CarIAs").GetComponent<Transform>();
        }

        // Start is called before the first frame update
        //void Start()
        //{

        //}

        // Update is called once per frame
        //void Update()
        //{
        //    DestroyBlockIfYOld();
        //}

        private IEnumerator DestroyBlockIfYOldCoroutine()
        {
            for (; ; )
            {
                if (carsIAs.Count == 0 && Camera.main.transform.position.y > yOld)
                    Destroy(gameObject);
                yield return null;
            }
        }

        //private void OnBecameInvisible()
        //{
        //    print($"{name} invisible");
        //}

        void LoadCarsIAs()
        {
            carsIAs = transformCarIAs.GetComponentsInChildren<CarIAController>().ToList();
            gameController.LoadCarIAs(carsIAs);
        }
    }
}