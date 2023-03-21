using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rbPlayer;

    [SerializeField] float boostMultiplier = 700f;
    [SerializeField] float rotationMultiplier = 100f;
    [SerializeField] float doubleBoostMultiplier = 2.0f;
    [SerializeField] float doubleBoostLerpTime = 1.0f;

    const float doubleBoostInterval = 0.2f;
    float lastBoostTime;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            MainBoost(); 
        }

        if (Input.GetKey(KeyCode.A))
        {
            RotateShip(rotationMultiplier);
        }

        if (Input.GetKey(KeyCode.D))
        {
            RotateShip(-rotationMultiplier);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            float boostInterval = Time.time - lastBoostTime;
            lastBoostTime = Time.time;

            if (boostInterval <= doubleBoostInterval)
            {
                DoubleBoost();
            }
            else
            {
                MainBoost();
            }
        }
    }

    private void RotateShip(float rotation)
    {
        rbPlayer.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        rbPlayer.freezeRotation = false;
    }

    private void MainBoost()
    {
        rbPlayer.AddRelativeForce(Vector3.up * boostMultiplier * Time.deltaTime, ForceMode.Force);
    }

    void DoubleBoost()
    {
        
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + transform.up * doubleBoostMultiplier;

        float travelPercent = 0.0f;

        while (travelPercent < 1.0f) 
        {
            travelPercent += Time.deltaTime * doubleBoostLerpTime;
            transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
        }
        
        float boostInterval = Time.time - lastBoostTime;
        lastBoostTime = Time.time;
        Debug.Log($"Last boost time: {boostInterval}");
    }

}
