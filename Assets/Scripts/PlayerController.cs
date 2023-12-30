using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    public class PlayerController : MonoBehaviour
    {
        // Components
        private Rigidbody2D rb;

        // Fields
        [SerializeField] private float speed;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            
        }

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {
            rb.velocity = speed * Vector2.up;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            print($"OnTriggerEnter2D: {collision.gameObject.name}");
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            print($"OnCollisionEnter2D: {collision.gameObject.name}");
        }
    }
}
