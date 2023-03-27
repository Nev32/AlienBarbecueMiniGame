using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rbPlayer;
    public Rigidbody RbPlayer { get { return rbPlayer; } }

    [SerializeField] float boostMultiplier = 700f;
    [SerializeField] float rotationMultiplier = 100f;
    [SerializeField] float doubleBoostMultiplier = 2.0f;
    [SerializeField] float doubleBoostLerpTime = 1.0f;
    [SerializeField] float beamMultiplier = 1.0f;
    [SerializeField] Transform beamOrigin;

    float beamDist = 5.0f;
    const float doubleBoostInterval = 0.2f;
    float lastBoostTime;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
    }

    
    void Update()
    {
        InputBoost();
        RotateShip();
        AbductionBeam();
    }

    void AbductionBeam()
    {
        RaycastHit hit;
        Ray beamRay = new Ray(beamOrigin.transform.position, -beamOrigin.transform.up);

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 stationaryPos = transform.position;
            
            Physics.Raycast(beamRay, out hit, beamDist);
            Debug.DrawRay(beamOrigin.transform.position, -beamOrigin.transform.up * beamDist, Color.red, 0.5f);
            
            if(!hit.collider.CompareTag("Cow")) { return; }
            else
            {
                rbPlayer.useGravity = false;

                Rigidbody rbCow = hit.rigidbody;
                rbCow.AddRelativeForce(Vector3.up * beamMultiplier * Time.deltaTime, ForceMode.Force);
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            rbPlayer.useGravity = true;
        }
    }

    private void RotateShip()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateShip(rotationMultiplier);
        }

        if (Input.GetKey(KeyCode.D))
        {
            RotateShip(-rotationMultiplier);
        }
    }

    private void InputBoost()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            MainBoost();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            float boostInterval = Time.time - lastBoostTime;

            if (boostInterval <= doubleBoostInterval)
            {
                StartCoroutine("DoubleBoost");
                Debug.Log("DOUBLE BOOST!");
            }
            lastBoostTime = Time.time;
        }
    }

    void RotateShip(float rotation)
    {
        rbPlayer.freezeRotation = true;
        transform.Rotate(Vector3.forward * rotation * Time.deltaTime);
        rbPlayer.freezeRotation = false;
    }

    void MainBoost()
    {
        rbPlayer.AddRelativeForce(Vector3.up * boostMultiplier * Time.deltaTime, ForceMode.Force);
    }

    IEnumerator DoubleBoost()
    {
        
        Vector3 startPos = transform.position;
        Vector3 endPos = startPos + transform.up * doubleBoostMultiplier;

        float travelPercent = 0.0f;

        while (travelPercent < 1.0f) 
        {
            transform.position = Vector3.Lerp(startPos, endPos, travelPercent);
            travelPercent += doubleBoostLerpTime * Time.deltaTime;
            yield return null;
        }
    }

}
