using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePattern : MonoBehaviour
{
    private float angle = 0f;
    private int numberSpirals = 2;
    private int bulletNumber = 10;
    private float startAngle = 90f, endAngle = 270f;
    public GameObject dragon;

    private Vector2 bulletMoveDirection;
    public List<string> callFunction = new List<string> {"Fire", "FireSpiral"};
    public List<float> functionInterval = new List<float> {0.1f, 2f};
    public List<float> executionTime = new List<float> {0.1f, 2f};

    public int sideShooting;
    int counter;
    float timer;
    float timerPhase;
    bool isStart;
    float startDelay;

    // Start is called before the first frame update
    void Start()
    {
        counter = 0;
        timer = 0;
        timerPhase = 0;
        isStart = true;
        startDelay = 4f;
        Invoke("setAttackAnimation", 1f);
        Invoke("playScream", 1.55f);
    }

    void setStart()
    {
        isStart = false;
        GameManager.instance.StartAnimationEnded();
    }

    void setAttackAnimation()
    {
        dragon.GetComponent<Animator>().SetTrigger("attack");
    }

    void playScream()
    {
        gameObject.GetComponent<bossController>().PlaySound("scream");
    }
    void Update()
    {
        if (!GameManager.instance.playing)
        {
            return;
        }

        timer += Time.deltaTime;
        timerPhase += Time.deltaTime;
        if (timerPhase > executionTime[counter])
        {
            timerPhase = 0;
            counter = (counter + 1) % executionTime.Count;
        }
    
        if (timer > functionInterval[counter])
        {
            if (isStart)
            {
                Invoke(callFunction[counter], startDelay);
                Invoke("setStart", startDelay);
            }
            else
            {
                setAttackAnimation();
                Invoke(callFunction[counter], 0f);
            }

            timer = 0f;
        }
    }

    private void Fire()
    {
        sideShooting = Random.Range(-2, 2);
        float angleStep = (endAngle - startAngle) / bulletNumber;
        float angle = startAngle;

        for (int i=0; i<bulletNumber+1; i++)
        {
            float bulletDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f) + sideShooting;
            float bulletDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
            Vector2 bulletDir = (bulletMoveVector - transform.position).normalized;

            GameObject bullet = BulletPool.bulletPoolInstance.GetBullet();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>().SetMoveDirection(bulletDir);

            angle += angleStep;
        }
    }

    private void FireStar()
    {
        sideShooting = Random.Range(-1, 1);
        bulletNumber = 20;
        float angleStep = (endAngle - startAngle) / bulletNumber;
        float angle = startAngle;

        for (int i=0; i<bulletNumber+1; i++)
        {
            float bulletDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 90f);
            float bulletDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 90f);

            Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
            Vector2 bulletDir = (bulletMoveVector - transform.position).normalized;

            GameObject bullet = BulletPool.bulletPoolInstance.GetBullet();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>().SetMoveDirection(bulletDir);

            angle += angleStep;
        }
    }

    private void FireSpiral()
    {
        float bulletDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
        float bulletDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

        Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
        Vector2 bulletDir = (bulletMoveVector - transform.position).normalized;

        GameObject bullet = BulletPool.bulletPoolInstance.GetBullet();
        bullet.transform.position = transform.position;
        bullet.transform.rotation = transform.rotation;
        bullet.SetActive(true);
        bullet.GetComponent<Bullet>().SetMoveDirection(bulletDir);

        angle += 10f;
    }

    private void FireDoubleSpiral()
    {
        for (int i=0; i<numberSpirals; i++)
        {
            float bulletDirX = transform.position.x + Mathf.Sin(((angle + 180f * i) * Mathf.PI) / 180f);
            float bulletDirY = transform.position.y + Mathf.Cos(((angle + 180f * i) * Mathf.PI) / 180f);

            Vector3 bulletMoveVector = new Vector3(bulletDirX, bulletDirY, 0f);
            Vector2 bulletDir = (bulletMoveVector - transform.position).normalized;

            GameObject bullet = BulletPool.bulletPoolInstance.GetBullet();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;
            bullet.SetActive(true);
            bullet.GetComponent<Bullet>().SetMoveDirection(bulletDir);
        }
        angle += 10f;

        if (angle >= 360f)
        {
            angle = 0f;
        }
    }
}
