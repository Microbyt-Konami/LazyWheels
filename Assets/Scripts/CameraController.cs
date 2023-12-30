using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels
{
    public class CameraController : MonoBehaviour
    {
        // Fields
        [SerializeField] private Transform target;

        // Components
        private Transform transformCamera;

        private void Awake()
        {
            transformCamera = GetComponent<Transform>();
        }

        // Update is called once per frame
        void Update()
        {
            transformCamera.position = new Vector3(transformCamera.position.x, target.position.y, transformCamera.position.z);
        }
    }
}
