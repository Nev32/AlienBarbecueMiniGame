using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowAnimatorController : MonoBehaviour
{
    Animator cowAnimator;
    public Animator CowAnimator { get { return cowAnimator; } }

    int isWalkingHash = Animator.StringToHash("isWalking");
    int isScaredHash = Animator.StringToHash("isScared");
    int isAbductingHash = Animator.StringToHash("isAbducting");

    void Start()
    {
        cowAnimator = GetComponent<Animator>();   
    }

    public void Walking()
    {
        bool animIsWalking = cowAnimator.GetBool(isWalkingHash);
        bool animIsScared = cowAnimator.GetBool(isScaredHash);
        bool animIsAbducting = cowAnimator.GetBool(isAbductingHash);

        animIsWalking = true;
        animIsScared = false;
        animIsAbducting = false;
    }

    public void Scared()
    {
        bool animIsWalking = cowAnimator.GetBool(isWalkingHash);
        bool animIsScared = cowAnimator.GetBool(isScaredHash);
        bool animIsAbducting = cowAnimator.GetBool(isAbductingHash);

        animIsWalking = false;
        animIsScared = true;
        animIsAbducting = false;
    }

    public void Idling()
    {
        bool animIsWalking = cowAnimator.GetBool(isWalkingHash);
        bool animIsScared = cowAnimator.GetBool(isScaredHash);
        bool animIsAbducting = cowAnimator.GetBool(isAbductingHash);

        animIsWalking = false;
        animIsScared = false;
        animIsAbducting = false;
    }

    public void Abducting()
    {
        bool animIsWalking = cowAnimator.GetBool(isWalkingHash);
        bool animIsScared = cowAnimator.GetBool(isScaredHash);
        bool animIsAbducting = cowAnimator.GetBool(isAbductingHash);

        animIsWalking = false;
        animIsScared = false;
        animIsAbducting = true;
    }
}

