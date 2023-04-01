using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour
{
    [SerializeField] ParticleSystem voidParticle;
    [SerializeField] float timeToDestroy = 1.5f;
    [SerializeField] GameObject cowMesh;

    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rbPlayer = collision.rigidbody;

        if (collision.collider.tag == "Player")
        {
            Instantiate(voidParticle, transform.position, Quaternion.identity);
            cowMesh.SetActive(false);
            Destroy(gameObject, timeToDestroy);
        }
    }
}