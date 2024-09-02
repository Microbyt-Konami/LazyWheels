using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels.Controllers
{
    [RequireComponent(typeof(CarController))]
    public class CarIAController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private bool alwaysLeft = true;

        //Components
        private CarController carController;
        private Transform myTransform;
        [Header("Components"), SerializeField] private BlockController blockController;
        [SerializeField] private Rays rays;

        //[Header("Variables")]
        //[SerializeField] private float direction = 0;

        public bool IsMoving
        {
            get => carController.IsMoving;
            set => carController.IsMoving = value;
        }

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
                //Move();
            }
        }

        void FixedUpdate()
        {
            if (IsMoving && !carController.IsExploding)
                Move();
        }

        private void ChangeBlockCurrent()
        {
            if (blockController != null)
            {
                var y = myTransform.position.y;

                if (y > blockController.YTop)
                {
                    var blockNext = GameController.Instance.FindBlockInY(y);

                    blockController.RemoveCarIA(this);
                    if (blockNext != null)
                    {
                        blockNext.ChangeParentToThisBlockCarsIAs(carController);
                        blockNext.AddCarIA(this);
                        blockController = blockNext;
                    }
                    else
                    {
                        blockController = null;
                        Destroy(gameObject);
                    }
                }
            }
        }

        private void Move()
        {
            var ray = carController.GetRay(Time.deltaTime * 2);
            var distanceLeft = rays.RayLeft.DistanceCollision;
            var distanceRight = rays.RayRight.DistanceCollision;

            if (distanceLeft >= 0 && rays.RayLineLeft.MetersLine >= 0 && rays.RayLineLeft.MetersLine < distanceLeft)
                distanceLeft = rays.RayLineLeft.MetersLine;
            if (distanceRight >= 0 && rays.RayLineRight.MetersLine >= 0 && rays.RayLineRight.MetersLine < distanceRight)
                distanceRight = rays.RayLineRight.MetersLine;

            var canMoveLeft = distanceLeft >= 0 && distanceLeft >= ray.x;
            var canMoveRight = distanceRight >= 0 && distanceRight >= ray.x;
            var distLeftBig = distanceLeft >= 0 && (distanceRight < 0 || distanceLeft >= distanceRight);

            if (rays.RayUp.DistanceCollision >= 0 && rays.RayUp.DistanceCollision <= ray.y)
            {
                if (distLeftBig && canMoveLeft)
                {
                    MoveLeft();

                    return;
                }
                else if (canMoveRight)
                {
                    MoveRight();

                    return;
                }
                else if (canMoveLeft)
                {
                    MoveLeft();

                    return;
                }

                if (distanceLeft >= 0 && distanceLeft < ray.x)
                {
                    if (canMoveRight)
                    {
                        MoveRight();

                        return;
                    }
                }

                if (distanceRight >= 0 && distanceRight < ray.x)
                {
                    if (canMoveLeft)
                    {
                        MoveLeft();

                        return;
                    }
                }

                if (alwaysLeft)
                {
                    if (canMoveRight)
                    {
                        if (carController.Line != null)
                        {
                            if (rays.RayLineRight.MetersLine > carController.Line.GetLineLastRight().Center(carController.Size.x))
                            {
                                MoveRight();

                                return;
                            }
                        }
                    }
                }
            }

            carController.Mover(0);

            void MoveRight()
            {
                carController.Mover(ray.x);
            }

            void MoveLeft()
            {
                carController.Mover(-ray.x);
            }
        }
    }
}