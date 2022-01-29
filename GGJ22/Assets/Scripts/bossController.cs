using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    public int health;
    int maxHealth = 10;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth / 2;
    }

    public void DecrementHealth()
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
            // WIN animation?
        }
    }

    public void IncrementHealth()
    {
        if ((health + 1) > maxHealth)
        {
            health = maxHealth;
            // YOU WIN BY HEALING THE BOSS
        }
        else
        {
            health++;
        }
    }

}
