using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rbPlayer = collision.rigidbody;

        if (collision.collider.tag == "Player")
        {
            rbPlayer.freezeRotation = true;
            Destroy(gameObject);
            rbPlayer.freezeRotation = false;
        }
    }
}