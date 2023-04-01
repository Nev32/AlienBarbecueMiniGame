using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour
{
    [SerializeField] ParticleSystem voidParticle;
    [SerializeField] AudioSource vanishingSound;
    [SerializeField] float timeToDestroy = 3.0f;
    [SerializeField] GameObject cowMesh;

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rbPlayer = collision.rigidbody;

        if (collision.collider.tag == "Player")
        {
            if (!vanishingSound.isPlaying)
            {
                vanishingSound.Play();
            }
            Instantiate(voidParticle, transform.position, Quaternion.identity);
            cowMesh.SetActive(false);
            Destroy(gameObject, timeToDestroy);
        }
    }
}