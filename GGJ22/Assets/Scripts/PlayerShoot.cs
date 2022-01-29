using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public Transform throwPoint;
    public GameObject magicalSpherePrefab;

    public float sphereForce = 20f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ThrowSphere();
        }
    }

    void ThrowSphere()
    {
        GameObject newSphere = Instantiate(magicalSpherePrefab, throwPoint.position, throwPoint.rotation);
        Rigidbody2D rb2D = newSphere.GetComponent<Rigidbody2D>();
        rb2D.AddForce(throwPoint.up * sphereForce, ForceMode2D.Impulse);

        Destroy(newSphere, 2f);
    }
}
