using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowMover : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1.5f;
    [SerializeField] float rotationSpeed = 1f;

    bool isWalkingRight = false;
    bool isWalkingLeft = false;
    bool isScared = false;

    bool isWandering = false;
    bool isGrounded = false;

    [SerializeField] Animator cowAnimator;
    
    void Update()
    {
        if (isGrounded)
        {
            ActivateMovement();
        }
        else
        {
            isGrounded = false;
            cowAnimator.SetBool("isWalking", false);
            cowAnimator.SetBool("isScared", false);
            cowAnimator.SetBool("isAbducting", true);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }

    private void ActivateMovement()
    {
        if (!isWandering)
        {
            StartCoroutine(Wander());
        }
        if (isWalkingRight)
        {
            RotateRight();
            MoveRight();
        }
        if (isWalkingLeft)
        {
            RotateLeft();
            MoveLeft();
        }
        if (!isWalkingLeft && !isWalkingRight)
        {
            int animationState = Random.Range(1, 3);

            if(cowAnimator == null) { return; }
            else
            {
                cowAnimator.SetBool("isWalking", false);
                cowAnimator.SetBool("isScared", false);
                cowAnimator.SetBool("isAbducting", false);
            }   
        }
        if (isScared && !isWalkingLeft && !isWalkingRight)
        {
            cowAnimator.SetBool("isWalking", false);
            cowAnimator.SetBool("isScared", true);
            cowAnimator.SetBool("isAbducting", false);
        }
    }

    IEnumerator Wander()
    {
        int walkTime = Random.Range(3, 6);
        int walkWait = Random.Range(6, 12);

        isWandering = true;

        yield return new WaitForSeconds(walkWait);
        isWalkingRight = true;
        yield return new WaitForSeconds(walkTime);
        isWalkingRight = false;
        int scaredShuffle = Random.Range(1, 4);
        if (scaredShuffle == 1)
        {
            isScared = true;
        }
        else
        {
            isScared = false;
        }
        yield return new WaitForSeconds(walkWait);
        isWalkingLeft = true;
        yield return new WaitForSeconds(walkTime);
        isWalkingLeft = false;
        int scaredShuffle2 = Random.Range(1, 4);
        if (scaredShuffle2 == 1)
        {
            isScared = true;
        }
        else
        {
            isScared = false;
        }

        isWandering = false;
    }

    void MoveRight()
    {
        cowAnimator.SetBool("isWalking", true);
        cowAnimator.SetBool("isScared", false);
        cowAnimator.SetBool("isAbducting", false);
        transform.position += transform.right * Time.deltaTime * moveSpeed;
    }

    void MoveLeft()
    {
        cowAnimator.SetBool("isWalking", true);
        cowAnimator.SetBool("isScared", false);
        cowAnimator.SetBool("isAbducting", false);
        transform.position += transform.right * Time.deltaTime * -moveSpeed;
    }

    void RotateRight()
    {
        Transform child = gameObject.transform.GetChild(0);
        child.transform.rotation = Quaternion.Euler(transform.rotation.x, -225, transform.rotation.z);
    }
    void RotateLeft()
    {
        Transform child = gameObject.transform.GetChild(0);
        child.transform.rotation = Quaternion.Euler(transform.rotation.x, -135, transform.rotation.z);
    }
}
