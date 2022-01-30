using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform throwPoint;
    public GameObject[] magicalSpherePrefab;

    public float sphereForce = 20f;
    float timer;
    float sphereSpawn;
    int maxSpheres;
    int numberSpheres;
    bool missingSpheres;
    public Queue<int> magicalSpheresList = new Queue<int>();

    void Start()
    {
        maxSpheres = 5;
        timer = 0;
        sphereSpawn = 5;
        missingSpheres = false;

        fillSpheres();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= sphereSpawn)
        {
            emptyFullSpheres();
            fillSpheres();
            timer = 0;
        }

        if (Input.GetButtonDown("Fire1") && !missingSpheres)
        {
            ThrowSphere(magicalSpheresList.Dequeue());
            numberSpheres--;
            GameManager.instance.RemovePlayerAmmo(numberSpheres);
            if (numberSpheres <= 0)
            {
                missingSpheres = true;
            }
        }
        else if (missingSpheres)
        {
            Debug.Log("No more fire");
        }
    }

    void fillSpheres()
    {
        List<int> flatMagicalList = new List<int>();
        for (int i=0; i<maxSpheres; i++)
        {
            int type = Random.Range(0, 2);
            magicalSpheresList.Enqueue(type);
            flatMagicalList.Add(type);
        }
        GameManager.instance.RefillPlayerAmmo(flatMagicalList);
        numberSpheres = maxSpheres;
        missingSpheres = false;
    }

    void emptyFullSpheres()
    {
        for (int i=0; i<numberSpheres; i++)
        {
            magicalSpheresList.Dequeue();
        }
        numberSpheres = 0;
        missingSpheres = true;
    }

    void ThrowSphere(int sphereType)
    {
        gameObject.GetComponent<PlayerController>().PlaySound("attack");

        GameObject newSphere = Instantiate(magicalSpherePrefab[sphereType], throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb2D = newSphere.GetComponent<Rigidbody2D>();
        rb2D.AddForce(throwPoint.up * sphereForce, ForceMode2D.Impulse);
        newSphere.gameObject.GetComponent<magicalSphere>().setSphereType(sphereType);

        Destroy(newSphere, 2f);
    }
}
