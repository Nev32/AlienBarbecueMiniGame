using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowController : MonoBehaviour
{
    [SerializeField] ParticleSystem voidParticle;
    [SerializeField] AudioSource vanishingSound;
    [SerializeField] GameObject cowMesh;

    [SerializeField] float timeToDestroy = 3.0f;
    public int scoreToAward = 1;

    ScoreCount scoreCount;

    private void Start()
    {
        scoreCount = FindAnyObjectByType<ScoreCount>();
    }

    public void AddScore()
    {
        scoreCount.IncreaseScore(scoreToAward);
    }

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
            AddScore();
            Destroy(gameObject, timeToDestroy);
        }
    }
}