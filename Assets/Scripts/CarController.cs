using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    public class CarController : MonoBehaviour
    {
        // Fields
        [SerializeField] private float speedMax = 200f * 1000 / (60f * 60f);
        [SerializeField] private float speed;
        [SerializeField] private float speedKm_h;

        // Components
        private Rigidbody2D rb;

        // Variables
        private float inputX;

        public void Mover(float inputX) => this.inputX = inputX;

        // acceleration (hm/h)
        public void Acceleration(float acceleration)
        {
            speedKm_h += acceleration;
            speed += 1000 * acceleration / (60 * 60);
            if (speed < 0)
                speedKm_h = speed = 0;
            if (speed > speedMax)
            {
                speed = speedMax;
                speedKm_h = speed * 60 * 60 / 1000;
            }
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
            rb.velocity = speed * (Vector2.up + inputX * Vector2.right);
        }
    }
}
