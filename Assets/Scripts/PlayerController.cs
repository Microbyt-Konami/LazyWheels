using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // GetComponent<Rigidbody2D>().velocity = Vector2.right;
    }

    // Update is called once per frame
    void Update()
    {

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
