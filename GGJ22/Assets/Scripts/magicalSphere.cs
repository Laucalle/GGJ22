using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class magicalSphere : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 lastVelocity;
    int sphereType;

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

    private int i = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("boss"))
        {
            if (i == 0)
            {
                Destroy(gameObject);
                if (sphereType == 0)
                {
                    other.gameObject.GetComponent<bossController>().IncrementHealth();
                }
                else
                {
                    other.gameObject.GetComponent<bossController>().DecrementHealth();
                }
                i++;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("boss"))
        {
            if (i == 1)
            {
                i = 0;
            }
        }
    }

    public void setSphereType(int type)
    {
        sphereType = type;
    }

    public int getSphereType()
    {
        return sphereType;
    }
}