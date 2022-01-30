using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public Rigidbody2D rb2D;
    public Camera cam;

    public int health;
    public Animator animator;
    int maxHealth = 5;

    Vector2 movement;
    Vector2 mousePos;
    Vector2 moveInput;

    void Start()
    {
        health = maxHealth;
    }

    public void DecrementHealth()
    {
        health--;
        animator.SetTrigger("hurt");
        if (health <= 0)
        {
            Destroy(gameObject);
            // GAME OVER animation?
        }
    }

    public void IncrementHealth()
    {
        if ((health + 1) > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health++;
        }
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        moveInput = new Vector2(movement.x, movement.y).normalized;
    
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("vel", moveInput.sqrMagnitude);
    }

    void FixedUpdate()
    {
        rb2D.MovePosition(rb2D.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb2D.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb2D.rotation = angle;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        int sphereType;
        if (other.gameObject.tag == "magicalSphere")
        {
            sphereType = other.gameObject.GetComponent<magicalSphere>().getSphereType();
            Destroy(other.gameObject);
            if (sphereType == 0)
            {
                IncrementHealth();
            }
            else
            {
                DecrementHealth();
            }
        }
    }

}