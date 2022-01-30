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
        if (!GameManager.instance.playing)
        {
            return;
        }

        health--;
        dragon.GetComponent<Animator>().SetTrigger("hurt");
        GameManager.instance.SetBossHealth(health, maxHealth);
        if (health <= 0)
        {
            GameManager.instance.EndGameWinKiller();
            Destroy(gameObject);
            // WIN animation?
            
        }
    }

    public void IncrementHealth()
    {
        if (!GameManager.instance.playing)
        {
            return;
        }

        healthPS.gameObject.GetComponent<ParticleSystem>().Play();
        if ((health + 1) > maxHealth)
        {
            GameManager.instance.EndGameWinHealer();
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
