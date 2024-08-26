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
        [SerializeField] private bool alwaysLeft = true;

        //Components
        private CarController carController;
        private Transform myTransform;
        [Header("Components"), SerializeField] private BlockController blockController;
        [SerializeField]private Rays rays;
        [SerializeField] private GameObject myCar;

        [Header("Variables")]
        [SerializeField] private float direction = 0;

        public bool IsMoving
        {
            get => carController.IsMoving;
            set => carController.IsMoving = value;
        }

        public GameObject MyCar => myCar;

        public void SetParent(Transform parent) => carController.SetParent(parent);

        private void Awake()
        {
            carController = GetComponent<CarController>();
            myTransform = GetComponent<Transform>();
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

        void FixedUpdate()
        {
            
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

            // if (distCarCastUp < ray.y || distCarCastUp < ray.y)
            // {
            //     if (distCarCastLeft < distCarCastRight)
            //         MoveRight();
            //     else
            //         MoveLeft();
            // }
            // else if (distCarCastLeft < ray.x)
            // {
            //     if (distCarCastLeft < DistCarCastRight)
            //         MoveRight();
            //     else
            //         MoveLeft();
            // }
            // else if (distCarCastRight < ray.x)
            // {
            //     if (distCarCastRight < distCarCastLeft)
            //         MoveLeft();
            //     else
            //         MoveRight();
            // }

            void MoveRight()
            {
                carController.Mover(-ray.x);
            }

            void MoveLeft()
            {
                carController.Mover(ray.x);
            }
        }
    }
}