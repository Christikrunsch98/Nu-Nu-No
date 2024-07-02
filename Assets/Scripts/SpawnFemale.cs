using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFemale : MonoBehaviour
{
    public GameObject FemalePrefab;
    public Vector3 SpawnPoint;
    public AudioSource PunchSound;
    public AudioSource TalkingSound;

    Rigidbody rb;
    Vector3 startPosition;
    float distance;
    bool death;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;

        int rnd = Random.Range(0, 10);
        if(rnd > 7) TalkingSound.Play();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(transform.position, startPosition);
        if(distance > 0.1f && !death)
        {
            death = true;
            StartCoroutine(Respawn());
            Destroy(this.gameObject, 5f);
        }
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(FemalePrefab, SpawnPoint, Quaternion.identity);
    }

    bool hit = false;

    private void OnCollisionEnter(Collision collision)
    {
        // Überprüfe, ob das kollidierte GameObject ein "Person" ist
        if (collision.gameObject.CompareTag("Boxing") && !hit)
        {
            collision.gameObject.GetComponent<BoxingGlove>().Punch(rb);
            if (PunchSound != null) PunchSound.Play();
        }
    }
}
