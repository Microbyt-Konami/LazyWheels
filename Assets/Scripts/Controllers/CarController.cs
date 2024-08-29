using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MicrobytKonami.Helpers;
using System.Runtime.InteropServices;
//using UnityEngine.Serialization;

namespace MicrobytKonami.LazyWheels.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CarController : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private BoxCollider2D boxColliderMyCar;
        [SerializeField] private float speedUp = 1;
        [SerializeField] private float speedRotation = 1;
        [field: SerializeField] public GameObject MyCar { get; private set; }
        [SerializeField] private CarExplode carExplode;

        // Components
        private Rigidbody2D rb;
        private BoxCollider2D collide;
        private Transform myTransform;
        private SpriteRenderer carSprite;
        [field: SerializeField, Header("Debug")] public LineController Line { get; private set; }

        // Variables        
        private bool isLockAccelerate, isInGrass;
        private Vector2 raySpeed, velocity;
        [SerializeField] private Vector2 sizeCollideMyCar;

        //[FormerlySerializedAs("isStop")]
        [SerializeField] private bool isMoving;

        private float inputX, inputOld;

        // Ids
        private int idGrassLayer, idObstacle, idCar, idLane;

        public bool IsMoving
        {
            get => isMoving;
            set => isMoving = value;
        }

        public Vector2 Size => boxColliderMyCar.size;

        public void Mover(float inputX)
        {
            this.inputX = inputX;
        }

        public void SetParent(Transform parent) => myTransform.parent = parent;

        public Vector2 GetRay(float seconds) => seconds * raySpeed;

        public void CarFade(float alpha)
        {
            var color = carSprite.color;

            color.a = alpha;
            carSprite.color = color;
        }

        public void StartCarFade(float time)
        {
            StartCoroutine(CarFadeCoroutine(time));
        }


        private void OnDisable()
        {
            Debug.Log($"{gameObject.name} disable", gameObject);
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            collide = GetComponent<BoxCollider2D>();
            myTransform = GetComponent<Transform>();
            idGrassLayer = LayerMask.NameToLayer("Grass");
            idObstacle = LayerMask.NameToLayer("Obstacle");
            idCar = LayerMask.NameToLayer("Car");
            idLane = LayerMask.NameToLayer("Lane");
            carSprite = MyCar.GetComponent<SpriteRenderer>();
            sizeCollideMyCar = boxColliderMyCar.size;
            CalcRaySpeed();
        }

        private void CalcRaySpeed()
        {
            raySpeed = new Vector2(speedRotation, speedUp);
            //velocity =
        }

        // Start is called before the first frame update
        //void Start()
        //{

        //}

        // Update is called once per frame
        void Update()
        {
            CalcRaySpeedIfChangeValues();
        }

        private void CalcRaySpeedIfChangeValues()
        {
            if (raySpeed.x != speedRotation || raySpeed.y != speedUp)
                CalcRaySpeed();
        }

        private void FixedUpdate()
        {
            if (isMoving)
            {
                //rb.velocity = speed * (Vector2.up + inputX * Vector2.right);
                //rb.velocity = new Vector2(speedMove * inputX, speed);

                float _speedRotation;
                float _speedUp;

                if (isInGrass)
                {
                    _speedUp = speedUp / 2f;
                    _speedRotation = speedRotation / 2f;
                    isInGrass = false;
                    CalcRaySpeed();
                }
                else
                {
                    _speedUp = speedUp;
                    _speedRotation = speedRotation;
                }

                rb.velocity = new Vector2(inputX * _speedRotation, _speedUp);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == idGrassLayer)
                isInGrass = true;
            else if (collision.gameObject.layer == idObstacle)
                Explode();
            else if (collision.gameObject.layer == idLane)
                Line = collision.gameObject.GetComponent<LineController>();
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == idGrassLayer)
                isInGrass = false;
            else if (collision.gameObject.layer == idLane)
                Line = null;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == idCar)
            {
                // De momento explotan los 2 coches.
                // Lo que se quedrá hacer es que explote el menos grande si hay un camión
                Explode();
                collision.gameObject.GetComponent<CarController>().Explode();
            }
        }

        //private void OnDrawGizmos()
        //{
        //    if (myTransform != null && collide != null)
        //    {
        //        var v1 = myTransform.position + (collide.bounds.size.y / 2) * Vector3.up;
        //        // calcular distancia segun velocidad
        //        var v2 = v1 + 10 * Vector3.up;

        //        Gizmos.color = Color.yellow;
        //        Gizmos.DrawLine(v1, v2);
        //    }
        //}

        private void Explode()
        {
            print($"Explode {name}");
            inputX = 0;
            rb.velocity = Vector2.zero;
            carExplode.Explode(this);
            /*
            // no forma no correcta es para chequear el choque
            if (gameObject.CompareTag("Player"))
            {
                transform.position -= transform.position.x * Vector3.right;
                gameObject.GetComponent<PlayerController>().Explode();
            }
            else
                Destroy(gameObject);
                */
        }

        private IEnumerator CarFadeCoroutine(float time)
        {
            var t = time;

            while (t > 0)
            {
                t -= Time.deltaTime;

                CarFade(t / time);

                yield return null;
            }
        }
    }
}