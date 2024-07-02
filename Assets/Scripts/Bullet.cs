using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f; // Geschwindigkeit der Kugel
    public float DestroyTime = 4f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

       rb.velocity = transform.forward * speed;

        // Destroy Bullet after DestroyTime so it never exists to long
        StartCoroutine(DestroyAfterTime(DestroyTime));
    }


    // Methode, die aufgerufen wird, wenn die Kugel mit etwas kollidiert
    private void OnTriggerEnter(Collider collision)
    {
        // Zerstöre die Kugel, wenn sie mit etwas kollidiert
        Destroy(gameObject);
    }

    IEnumerator DestroyAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
