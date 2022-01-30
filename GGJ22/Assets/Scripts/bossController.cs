using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossController : MonoBehaviour
{
    public int health;
    int maxHealth = 10;
    public GameObject healthPS;
    public GameObject dragon;

    public AudioClip hurt, heal, scream, deadEnd, healedEnd;
    public AudioSource audioSrc;
    public GameObject gameMgr;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth / 2;
        audioSrc = GetComponent<AudioSource>();
    }

    public void PlaySound (string clip)
    {
        switch(clip)
        {
            case "hurt":
                audioSrc.pitch = Random.Range(0.8f, 1.1f);
                audioSrc.PlayOneShot(hurt);
                break;
            case "heal":
                audioSrc.PlayOneShot(heal);
                break;
            case "scream":
                audioSrc.PlayOneShot(scream);
                break;
            case "deadEnd":
                audioSrc.PlayOneShot(deadEnd);
                break;
            case "healedEnd":
                audioSrc.PlayOneShot(healedEnd);
                break;
        }
    }

    public void DecrementHealth()
    {
        if (!GameManager.instance.playing)
        {
            return;
        }
        PlaySound("hurt");
        health--;
        dragon.GetComponent<Animator>().SetTrigger("hurt");
        GameManager.instance.SetBossHealth(health, maxHealth);
        if (health <= 0)
        {
            PlaySound("deadEnd");
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
        PlaySound("heal");
        healthPS.gameObject.GetComponent<ParticleSystem>().Play();
        if ((health + 1) > maxHealth)
        {
            PlaySound("healedEnd");
            gameMgr.GetComponent<AudioSource>().Stop();
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
