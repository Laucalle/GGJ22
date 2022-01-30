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
    public GameObject threshold;
    public GameObject healthPS;
    int maxHealth = 5;
    float lastY;

    Vector2 movement;
    Vector2 mousePos;
    Vector2 moveInput;
    public bool beingHurt;
    public bool beingStunned;

    void Start()
    {
        health = maxHealth;
    }

    void setBeingHurt()
    {
        beingHurt = false;
    }

    void setBeingStunned()
    {
        beingStunned = false;
    }

    public bool getBeingHurt()
    {
        return beingHurt;
    }

    public void DecrementHealth()
    {
        health--;
        animator.SetTrigger("hurt");
        beingHurt = true;
        beingStunned = true;
        Invoke("setBeingHurt",0.583f);
        Invoke("setBeingStunned",0.3f);
        GameManager.instance.SetPlayerHealth(health, maxHealth);
        if (health <= 0)
        {
            Destroy(gameObject);
            // GAME OVER animation?
            
        }
    }

    public void IncrementHealth()
    {
        healthPS.gameObject.GetComponent<ParticleSystem>().Play();
        if ((health + 1) > maxHealth)
        {
            health = maxHealth;
        }
        else
        {
            health++;
        }
        GameManager.instance.SetPlayerHealth(health, maxHealth);
    }

    void Update()
    {
        
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    
        if (transform.position.y > threshold.transform.position.y && movement.y > 0)
        {
            movement.y = 0;
        }

        if (beingStunned)
        {
            movement.x = 0;
            movement.y = 0;
        }

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    
        moveInput = new Vector2(movement.x, movement.y).normalized;
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
                Debug.Log(beingHurt);
                if (!beingHurt)
                {
                    DecrementHealth();
                }
            }
        }
    }

}