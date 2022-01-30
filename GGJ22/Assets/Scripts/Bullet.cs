using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector2 moveDirection;
    private float moveSpeed;

    private void OnEnable()
    {
        Invoke("Destroy", 3f);
    }

    void Start()
    {
        moveSpeed = 10f;
    }

    void Update()
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
    }

    public void SetMoveDirection(Vector2 dir)
    {
        moveDirection = dir;
    }

    private void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool beingHurt;
        if (other.CompareTag("player"))
        {
            Destroy();
            beingHurt = other.GetComponent<PlayerController>().getBeingHurt();
            if (!beingHurt)
            {
                other.GetComponent<PlayerController>().DecrementHealth();
            }
        }
    }
}