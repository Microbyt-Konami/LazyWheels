using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using MicrobytKonami.LazyWheels.Helpers;

namespace MicrobytKonami.LazyWheels
{
    public class CarController : MonoBehaviour
    {
        // Fields
        [SerializeField] private float speedMax = HelperUnitsConvertions.KmHToMS(200f);
        [SerializeField] private float speedLow = HelperUnitsConvertions.KmHToMS(30);
        [SerializeField] private float forceMotor = 1;

        // Components
        private Rigidbody2D rb;

        // Variables
        private bool isInput;
        private float inputX, inputY;
        [SerializeField] private float speed;
        [SerializeField] private float speedKm_h;
        [SerializeField] private float speedMove;

        public void Mover(float inputX)
        {
            isInput = true;
            this.inputX = inputX;
        }

        public void Acceleration(float inputY)
        {
            isInput = true;
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
            isInput = true;
            this.inputY = inputY == 0 ? -1 : inputY;
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
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
            if (isInput)
            {
                rb.AddForce(new Vector2(0, inputY * forceMotor));
                speed = rb.velocity.y;
                if (speed < 0)
                    speed = 0;
                else if (speed > speedMax)
                    speed = speedMax;
                speedMove = speed <= speedLow ? speed : speed / speedLow;
                rb.velocity = new Vector2(speedMove * inputX, speed);
                speedKm_h = HelperUnitsConvertions.MSToKmH(speed);
                isInput = false;
            }
        }
    }
}
