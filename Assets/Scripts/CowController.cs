using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour
{
    //[SerializeField] ParticleSystem voidParticle;
    
    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rbPlayer = collision.rigidbody;

        if (collision.collider.tag == "Player")
        {
            rbPlayer.freezeRotation = true;
            //Instantiate(voidParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
            rbPlayer.freezeRotation = false;
        }
    }
}