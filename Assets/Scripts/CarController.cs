using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    public class CarController : MonoBehaviour
    {
        // Fields
        [SerializeField] private float speed;

        // Components
        private Rigidbody2D rb;

        // Variables
        private float inputX;

        public void Mover(float inputX) => this.inputX = inputX;

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
