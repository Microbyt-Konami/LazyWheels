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

        // Components
        private Rigidbody2D rb;

        // Variables
        private float inputX;
        [SerializeField] private float speed;
        [SerializeField] private float speedLow = HelperUnitsConvertions.KmHToMS(30);
        [SerializeField] private float speedKm_h;
        [SerializeField] private float speedMove;

        public void Mover(float inputX) => this.inputX = inputX;

        // acceleration (hm/h)
        public void Acceleration(float acceleration)
        {
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
        }

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
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
            rb.velocity = new Vector2(speedMove * inputX, speed);
        }
    }
}
