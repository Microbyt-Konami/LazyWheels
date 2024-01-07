using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using MicrobytKonami.LazyWheels.Helpers;

namespace MicrobytKonami.LazyWheels.Controllers
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CarController : MonoBehaviour
    {
        // Fields
        [SerializeField] private float speedMax = HelperUnitsConvertions.KmHToMS(200f);
        [SerializeField] private float speedLow = HelperUnitsConvertions.KmHToMS(30);
        [SerializeField] private float forceMotor = 1;

        // Components
        private Rigidbody2D rb;

        // Variables
        private bool isInput, isLockAccelerate, isInGrass;
        private float inputX, inputY;
        [SerializeField] private float speed;
        [SerializeField] private float speedKm_h;
        [SerializeField] private float speedMove;
        private float oldInputY;

        // Ids
        private int idGrassLayer, idObstacle, idPlayer;

        public void Mover(float inputX)
        {
            isInput = true;
            this.inputX = inputX;
        }

        public void Acceleration(float inputY)
        {
            if (isLockAccelerate && inputY > 0)
                return;

            isInput = true;
            oldInputY = this.inputY;
            this.inputY = inputY;
            /*
            speedKm_h += acceleration;
            speed += HelperUnitsConvertions.KmHToMS(acceleration);
            if (speed < 0)
                speedKm_h = speed = 0;
            if (speed > speedMax)
            {
                speed = speedMax;
                speedKm_h = HelperUnitsConvertions.MSToKmH(speed);
            }
            speedMove = speed <= speedLow ? speed : speed / speedLow;
            */
        }

        public void IAcceleration(float inputY)
        {
            if (isLockAccelerate && inputY > 0)
                return;

            var _oldInputY = oldInputY;

            oldInputY = this.inputY;
            isInput = true;

            this.inputY =
                inputY == 0 && _oldInputY == 0
                    ? -1
                    : _oldInputY <= inputY
                        ? inputY
                        : inputY - 1;
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            idGrassLayer = LayerMask.NameToLayer("Grass");
            idObstacle = LayerMask.NameToLayer("Obstacle");
            idPlayer = LayerMask.NameToLayer("Player");
            isInput = false;
            speed = speedKm_h = speedMove = 0;
        }

        // Start is called before the first frame update
        //void Start()
        //{

        //}

        // Update is called once per frame
        //void Update()
        //{

        //}

        private void FixedUpdate()
        {
            //rb.velocity = speed * (Vector2.up + inputX * Vector2.right);
            //rb.velocity = new Vector2(speedMove * inputX, speed);
            DeacelerateGrass();
            AccelerateAndMove();
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
            speed = speedMove = speedKm_h = 0;
            rb.velocity = Vector2.zero;
            ResetInputY();
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

        private void AccelerateAndMove()
        {
            if (isInput)
            {
                if (speed <= 0 && inputY < 0)
                {
                    isLockAccelerate = false;
                    speed = 0;
                    ResetInputY();
                }
                else
                {
                    rb.AddForce(new Vector2(0, inputY * forceMotor));
                    speed = rb.velocity.y;
                    if (speed < 0)
                    {
                        isLockAccelerate = false;
                        speed = 0;
                        ResetInputY();
                    }
                    else if (speed > speedMax)
                    {
                        isLockAccelerate = true;
                        speed = speedMax;
                        ResetInputY();
                    }
                    else
                        isLockAccelerate = false;

                }
                speedMove = speed <= speedLow ? speed : speed / speedLow;
                rb.velocity = new Vector2(speedMove * inputX, speed);
                speedKm_h = HelperUnitsConvertions.MSToKmH(speed);
                isInput = false;
            }
        }

        private void DeacelerateGrass()
        {
            if (isInGrass)
            {
                if (speed > speedLow)
                {
                    var gameController = GameController.Instance;

                    oldInputY = inputY;
                    inputX = -gameController.InputXDeacelerateGrass;
                    inputY = -gameController.InputYDeacelerateGrass;
                    isInput = true;
                }
            }
        }

        private void ResetInputY()
        {
            rb.AddForce(Vector2.zero);
            oldInputY = inputY = 0;
        }
    }
}
