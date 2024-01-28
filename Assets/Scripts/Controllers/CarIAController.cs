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
        [SerializeField] private bool canChangeLane;

        //Components
        private CarController carController;
        private Transform myTransform;
        [SerializeField] private BlockController blockController;
        private Rays rays; 
        [SerializeField] private GameObject[] _capsuleObjs;

        public GameObject[] CapsuleObjs { get => _capsuleObjs; }

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
            rays = GetComponent<Rays>();
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

            //Physics2D.Raycast
            if (canChangeLane)
            {
                ChangeLineForNoCrash();
            }
        }

        private void ChangeLineForNoCrash()
        {

        }
    }
}