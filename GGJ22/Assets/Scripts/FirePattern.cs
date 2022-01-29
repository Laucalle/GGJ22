using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePattern : MonoBehaviour
{
    private float angle = 0f;
    private int numberSpirals = 2;
    private int bulletNumber = 10;
    private float startAngle = 90f, endAngle = 270f;

    private Vector2 bulletMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("FireDoubleSpiral", 0f, 1f);
    }

    private void Fire()
    {
        float angleStep = (endAngle - startAngle) / bulletNumber;
        float angle = startAngle;

        for (int i=0; i<bulletNumber+1; i++)
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
