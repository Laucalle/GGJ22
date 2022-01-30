using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public bool playing;
    public bool endGame;
    public bool startAnimPlaying;

    // UI TINGS
    public List<GameObject> hearts;
    public List<GameObject> ammo;
    public Image bossBar;
    public Image bossBarFX;
    public AnimationCurve bossBarCurve;
    Image laggingBar;
    float targetHealth;
    public float bossHealthAnimationDuration = 0.5f;

    public GameObject panelGAMEOVER;
    public GameObject panelKILLERWIN;
    public GameObject panelHEALERWIN;

    void OnEnable()
    {
        if(instance != null)
        {
            Destroy(instance);
        }
        instance = this;
        BeginGame();
        endGame = false;
        startAnimPlaying = false; // TODO: SET TO TRUE!
        // possibly call initit things on the boss 
    }
    // Start is called before the first frame update
    void Start()
    {
        // Start music and stff
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
        }
        if (endGame)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
    void BeginGame()
    {
        playing = true;
    }

    public void StartAnimationEnded()
    {
        startAnimPlaying = false;
    }

    public void EndGameOver()
    {
        // Put Up End Screen
        panelGAMEOVER.SetActive(true);
        playing = false;
        endGame = true;
    }
    public void EndGameWinKiller()
    {
        // Put Up End Screen
        panelKILLERWIN.SetActive(true);
        playing = false;
        endGame = true;
    }
    public void EndGameWinHealer()
    {
        // Put Up End Screen
        panelHEALERWIN.SetActive(true);
        playing = false;
        endGame = true;
    }

    public void SetPlayerHealth(float health, float maxhealth)
    {
        float scaledhealth = (health * hearts.Count) / maxhealth;

        int fullHearts = (int) Mathf.Floor(scaledhealth);
        float partial = (float) (scaledhealth - fullHearts);
        int counter = 0;
        while (counter < fullHearts)
        {
            Image image = hearts[counter].transform.GetChild(1).gameObject.GetComponent<Image>();
            if(image.fillAmount != 1)
            {
                hearts[counter].GetComponent<Animator>().SetTrigger("onChange");
            }
            image.fillAmount = 1;
            counter++;
        }
        if (counter < hearts.Count) {
            Image image = hearts[counter].transform.GetChild(1).gameObject.GetComponent<Image>();
            if (image.fillAmount != partial)
            {
                hearts[counter].GetComponent<Animator>().SetTrigger("onChange");
            }
            image.fillAmount = partial;
            counter++;
        }
        while (counter < hearts.Count)
        {
            Image image = hearts[counter].transform.GetChild(1).gameObject.GetComponent<Image>();
            if (image.fillAmount != 0)
            {
                hearts[counter].GetComponent<Animator>().SetTrigger("onChange");
            }
            image.fillAmount = 0;
            counter++;
        }
    }

    // 0 - health
    // 1 - damage

    public void RefillPlayerAmmo(List<int> magical) {
        int heal = 0;
        int hurt = 1;
        magical.Reverse(); // IT IS A FIFO BUT WE FILL THE OTHER WAY
        for(int i = 0;i<ammo.Count; i++)
        {
            if (i < magical.Count)
            {
                if(magical[i] == heal)
                {
                    ammo[i].transform.GetChild(heal).gameObject.SetActive(true);
                    ammo[i].transform.GetChild(hurt).gameObject.SetActive(false);
                }
                if (magical[i] == hurt)
                {
                    ammo[i].transform.GetChild(hurt).gameObject.SetActive(true);
                    ammo[i].transform.GetChild(heal).gameObject.SetActive(false);
                }
                ammo[i].GetComponent<Animator>().SetTrigger("refill");
            }
        }
    }
    public void RemovePlayerAmmo(int index)
    {
        int heal = 0;
        int hurt = 1;
        if (index < ammo.Count)
        {
            ammo[index].transform.GetChild(heal).gameObject.SetActive(false);
            ammo[index].transform.GetChild(hurt).gameObject.SetActive(false);
            ammo[index].GetComponent<Animator>().SetTrigger("remove");
        }
    }

    public void SetBossHealth(int health, int maxhealth)
    {
        float target = (float)health/(float)maxhealth;
        float current = targetHealth;
        if(current < target)
        {
            //healing
            bossBarFX.fillAmount = target;
            laggingBar = bossBar;
        } else
        {
            //hurting
            bossBar.fillAmount = target;
            laggingBar = bossBarFX;
        }
        targetHealth = target;
        StartCoroutine(LagBarFill());
       
    }

    private IEnumerator LagBarFill()
    {
        float initialfill = laggingBar.fillAmount;
        float i = 0;
        float rate = 1 / bossHealthAnimationDuration;
        while (i < 1)
        {
            i += rate * Time.deltaTime;
            laggingBar.fillAmount = Mathf.Lerp(initialfill, targetHealth, bossBarCurve.Evaluate(i));
            yield return 0;
        }
    }
}
