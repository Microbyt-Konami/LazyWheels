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
        private Camera theCamera;
        private Transform transformCamera;

        // Variables
        private float height;

        public float Height => height;

        private void Awake()
        {
            theCamera = GetComponent<Camera>();
            transformCamera = GetComponent<Transform>();
            //height = theCamera.ScreenToWorldPoint(new Vector3(0, theCamera.pixelHeight / 2f, 0)).y - 0.5f;
            height = theCamera.orthographicSize;
        }

        // Start is called before the first frame update
        //void Start()
        //{
        //    //height = theCamera.ScreenToWorldPoint(new Vector3(0, 0, 0)).y - 0.5f;
        //}

        // Update is called once per frame
        //void Update()
        //{
        //}

        private void LateUpdate()
        {
            transformCamera.position = new Vector3(transformCamera.position.x, target.position.y, transformCamera.position.z);
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.green;
        //    //Gizmos.DrawLine(transform.position, Camera.main.ScreenToViewportPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0)));
        //    Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, Camera.main.orthographicSize, transform.position.y));
        //}
    }
}
