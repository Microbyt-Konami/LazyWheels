using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MicrobytKonami.Helpers;
using System.Runtime.InteropServices;
using UnityEngine.InputSystem.XR;
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
        [SerializeField] private GameObject carExplode;
        [SerializeField] private GameObject carFlame;

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
        [field: SerializeField] public bool IsExploding { get; private set; }

        private float inputX, inputOld;

        // Ids
        private int idGrassLayer, idObstacle, idCar, idLane;

        public bool IsMoving
        {
            get => isMoving;
            set => isMoving = value;
        }

        public BoxCollider2D BoxColliderCar => boxColliderMyCar;

        public Vector2 Size => boxColliderMyCar.size;

        public float Weight => rb.mass;

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

        public void StartCarFade(float time, float untilTime = 0)
        {
            StartCoroutine(CarFadeCoroutine(time));
        }

        public void Explode()
        {
            if (IsExploding)
                return;

            IsExploding = true;
            print($"Explode {name}");
            //IsMoving = false;
            //inputX = 0;
            rb.velocity = Vector2.zero;

            CarFade(0);

            var goExplode = Instantiate(carExplode, myTransform.position, Quaternion.identity, myTransform);
            var explode = goExplode.GetComponent<CarExplode>();

            explode.OnExplodeEnd.AddListener(ExplodeEnd);

            //StartCarFade(explode.Duration, explode.Duration / 4);

            //Destroy(gameObject);

            //carExplode.Explode(this);
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

            void ExplodeEnd()
            {
                Debug.Log("ExplodeEnd");
                //Debug.Break();

                IsMoving = false;
                IsExploding = false;
                if (TryGetComponent<PlayerController>(out var player))
                    player.Die();
                else
                {
                    Instantiate(carFlame, myTransform.position, Quaternion.Euler(-90, 0, 0), GetComponentInParent<BlockController>()?.GetComponent<Transform>());
                    Destroy(gameObject);
                }
            }
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

        //Start is called before the first frame update
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
                ExplodeCars(this, collision.gameObject.GetComponent<CarController>());
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

        private void ExplodeCars(CarController car1, CarController car2)
        {
            // Lo que se quedrá hacer es que explote el menos grande si hay un camión
            if (car1.Weight > car2.Weight && car1.Weight / car2.Weight > 10)
                car2.Explode();
            else if (car2.Weight > car1.Weight && car2.Weight / car1.Weight > 10)
                car1.Explode();
            else
            {
                car1.Explode();
                car2.Explode();
            }
        }

        private IEnumerator CarFadeCoroutine(float time, float untilTime = 0)
        {
            var t = time;

            while (t > untilTime)
            {
                t -= Time.deltaTime;

                CarFade(t / time);

                yield return null;
            }
        }
    }
}