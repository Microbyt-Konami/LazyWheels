using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MicrobytKonami.LazyWheels.Input;

namespace MicrobytKonami.LazyWheels
{
    public class PlayerController : MonoBehaviour
    {
        // Components
        private CarController carController;
        private InputActions inputActions;

        private void Awake()
        {
            carController = GetComponent<CarController>();
            inputActions = new InputActions();
            //inputActions.Player.Move.performed += ctx => carController.Mover(ctx.ReadValue<float>());
        }

        // Start is called before the first frame update
        //void Start()
        //{

        //}

        // Update is called once per frame
        void Update()
        {
            carController.Mover(inputActions.Player.Move.ReadValue<float>());
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
