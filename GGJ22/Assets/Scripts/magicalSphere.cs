using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicalSphere : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 lastVelocity;

    // public GameObject hitEffect; // Add animation for the magical sphere hitting something!!

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        var speed = lastVelocity.magnitude;
        var direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        rb.velocity = direction * Mathf.Max(speed, 0f);
        //GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        //Destroy(effect, 5f);
        Destroy(gameObject,2f);
    }

    void OnTriggetEnter2D(Collider2D col)
    {
        if (col.tag == "boss")
        {
            Debug.Log("collisioning with boss");
            Destroy(gameObject);
        }
    }
}