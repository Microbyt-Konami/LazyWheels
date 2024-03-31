using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels.Controllers
{
    [RequireComponent(typeof(CarController))]
    public class CarIAController : MonoBehaviour
    {
        // Fields
        [SerializeField] private bool canMoveLeft = true;
        [SerializeField] private bool canMoveRight = true;

        //Components
        private CarController carController;
        private Transform myTransform;
        [Header("Components"), SerializeField] private BlockController blockController;
        private Rays _rays;
        [SerializeField] private GameObject myCar;

        [Header("Variables")]
        [SerializeField, Tooltip("Distances Car Cast Up (<0 no cast)")] private float distCarCastUp = -1;
        [SerializeField, Tooltip("Distances Car Cast Down (<0 no cast)")] private float distCarCastDown = -1;
        [SerializeField, Tooltip("Distances Car Cast Left (<0 no cast)")] private float distCarCastLeft = -1;
        [SerializeField, Tooltip("Distances Car Cast Right (<0 no cast)")] private float distCarCastRight = -1;

        public bool IsMoving
        {
            get => carController.IsMoving;
            set => carController.IsMoving = value;
        }
        public GameObject MyCar => myCar;

        public Rays Rays => _rays;

        public float DistCarCastUp { get => distCarCastUp; set => distCarCastUp = value; }
        public float DistCarCastDown { get => distCarCastDown; set => distCarCastDown = value; }
        public float DistCarCastLeft { get => distCarCastLeft; set => distCarCastLeft = value; }
        public float DistCarCastRight { get => distCarCastRight; set => distCarCastRight = value; }

        public void SetParent(Transform parent) => carController.SetParent(parent);

        private void Awake()
        {
            carController = GetComponent<CarController>();
            myTransform = GetComponent<Transform>();
            _rays = GetComponent<Rays>();
            IsMoving = false;
        }

        // Start is called before the first frame update
        void Start()
        {
            blockController = GetComponentInParent<BlockController>();
            carController.Mover(0);
        }

        private void OnDestroy()
        {
            blockController?.RemoveCarIA(this);
        }

        // Update is called once per frame
        void Update()
        {
            if (IsMoving)
            {
                ChangeBlockCurrent();
                Move();
            }
        }

        private void ChangeBlockCurrent()
        {
            if (blockController != null)
            {
                var y = myTransform.position.y;

                if (y > blockController.YTop)
                {
                    var blockNext = GameController.Instance.FindBlockInY(y);

                    if (blockNext != null)
                    {
                        blockController.RemoveCarIA(this);
                        blockNext.AddCarIA(this);
                        blockController = blockNext;
                    }
                }
            }
        }

        private void Move()
        {
            var ray = carController.GetRay(3 * Time.deltaTime);

            if (distCarCastUp < ray.y || distCarCastUp < ray.y)
            {
                if (distCarCastLeft < distCarCastRight)
                    MoveRight();
                else
                    MoveLeft();
            }
            else if (distCarCastLeft < ray.x)
            {
                if (distCarCastLeft < DistCarCastRight)
                    MoveRight();
                else
                    MoveLeft();
            }
            else if (distCarCastRight < ray.x)
            {
                if (distCarCastRight < distCarCastLeft)
                    MoveLeft();
                else
                    MoveRight();
            }

            void MoveRight()
            {
                carController.Mover(-ray.x);
                distCarCastLeft -= ray.x;
                distCarCastRight += ray.x;
            }

            void MoveLeft()
            {
                carController.Mover(ray.x);
                distCarCastLeft += ray.x;
                distCarCastRight -= ray.x;
            }
        }
    }
}