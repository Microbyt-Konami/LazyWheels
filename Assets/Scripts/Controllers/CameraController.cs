using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MicrobytKonami.LazyWheels.Controllers
{
    public class CameraController : MonoBehaviour
    {
        // Fields
        [SerializeField] private Transform target;
        [SerializeField] private float timeCarAIMoving = 0.1f;

        // Components
        private Camera theCamera;
        private Transform transformCamera;
        private BoxCollider2D boxCollider2D;

        // Variables
        private float width, height;

        public float Height => height;

        private void Awake()
        {
            theCamera = GetComponent<Camera>();
            transformCamera = GetComponent<Transform>();
            boxCollider2D = GetComponent<BoxCollider2D>();
            height = theCamera.orthographicSize;
            width = height * theCamera.aspect;
            boxCollider2D.size = new Vector2(width * 2, height * 2);
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

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("CarIA") && other.gameObject.transform.parent.TryGetComponent<CarIAController>(out var carIAController) && !carIAController.IsMoving)
            {
                //carIAController.IsMoving = true;
                StartCoroutine(SetCarAIMoving(carIAController));
            }
        }

        //private void OnDrawGizmos()
        //{
        //    Gizmos.color = Color.green;
        //    //Gizmos.DrawLine(transform.position, Camera.main.ScreenToViewportPoint(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2, 0)));
        //    Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, Camera.main.orthographicSize, transform.position.y));
        //}

        private IEnumerator SetCarAIMoving(CarIAController carIAController)
        {
            yield return new WaitForSeconds(timeCarAIMoving);

            carIAController.StartRunCar();
        }
    }
}
