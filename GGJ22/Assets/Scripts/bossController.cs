using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    public int health;
    int maxHealth = 10;
    public GameObject healthPS;
    public GameObject dragon;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth / 2;
    }

    public void DecrementHealth()
    {
        health--;
        dragon.GetComponent<Animator>().SetTrigger("hurt");
        GameManager.instance.SetBossHealth(health, maxHealth);
        if (health <= 0)
        {
            Destroy(gameObject);
            // WIN animation?
        }
    }

    public void IncrementHealth()
    {
        healthPS.gameObject.GetComponent<ParticleSystem>().Play();
        if ((health + 1) > maxHealth)
        {
            health = maxHealth;
            // YOU WIN BY HEALING THE BOSS
        }
        else
        {
            health++;
        }
        GameManager.instance.SetBossHealth(health, maxHealth);
    }

}
