using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MicrobytKonami.Helpers;

namespace MicrobytKonami.LazyWheels.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CarController : MonoBehaviour
    {
        // Fields
        [SerializeField] private float speedUp = 1;
        [SerializeField] private float speedRotation = 1;
        [SerializeField] private float angleRotation = 1;

        // Components
        private Rigidbody2D rb;
        private Transform transformCar;

        // Variables
        private bool isLockAccelerate, isInGrass;
        private float inputX, inputXOld, rotationZEnd, rotationZ;

        // Ids
        private int idGrassLayer, idObstacle, idPlayer;

        public void Mover(float inputX)
        {
            this.inputX = inputX;
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            transformCar = GetComponent<Transform>();
            idGrassLayer = LayerMask.NameToLayer("Grass");
            idObstacle = LayerMask.NameToLayer("Obstacle");
            idPlayer = LayerMask.NameToLayer("Player");
        }

        // Start is called before the first frame update
        //void Start()
        //{

        //}

        // Update is called once per frame
        void Update()
        {
            if (inputX != inputXOld)
                rotationZEnd = inputX * -angleRotation;
            if (rotationZEnd > 0)
            {
                rotationZ += angleRotation * Time.deltaTime;
                if (rotationZ >= rotationZEnd)
                    rotationZ = rotationZEnd;
            }
            else
            {
                rotationZ -= angleRotation * Time.deltaTime;
                if (rotationZ <= rotationZEnd)
                    rotationZ = rotationZEnd;
            }

            transformCar.rotation = Quaternion.Euler(0, 0, rotationZEnd);

            inputXOld = inputX;
        }

        private void FixedUpdate()
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
            }
            else
            {
                _speedUp = speedUp;
                _speedRotation = speedRotation;
            }

            rb.velocity = new Vector2(inputX * _speedRotation, _speedUp);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == idGrassLayer)
                isInGrass = true;
            else if (collision.gameObject.layer == idObstacle)
                Explode();
        }

        private void Explode()
        {
            print($"Explode {name}");
            inputX = 0;
            rb.velocity = Vector2.zero;
            // no forma no correcta es para chequear el choque
            if (gameObject.name == "Player")
                transform.position -= transform.position.x * Vector3.right;
            else
                Destroy(gameObject);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == idGrassLayer)
                isInGrass = false;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == idPlayer)
            {
                collision.gameObject.GetComponent<CarController>().Explode();
                Explode();
            }
        }
    }
}