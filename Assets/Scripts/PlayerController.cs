using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rbPlayer;
    public Rigidbody RbPlayer { get { return rbPlayer; } }

    [Header("Saucer Flying settings:")]
    [Tooltip("Speed of main boost:")] [SerializeField] float boostMultiplier = 700f;
    [Tooltip("Speed of turning:")] [SerializeField] float rotationMultiplier = 100f;
    [Tooltip("Dash distance:")] [SerializeField] float doubleBoostMultiplier = 2.0f;
    [Tooltip("Dash Speed:")] [SerializeField] float doubleBoostLerpTime = 1.0f;
    [SerializeField] ParticleSystem mainBoostParticles;
    [SerializeField] ParticleSystem dashParticles;

    [Header("Saucer Beam settings:")]
    [Tooltip("Abduction beam strength:")] [SerializeField] float beamMultiplier = 1.0f;
    [Tooltip("Abduction beam distance:")] float beamDist = 5.0f;
    [SerializeField] Transform beamOrigin;
    [SerializeField] ParticleSystem beamParticles;

    const float doubleBoostInterval = 0.2f;
    float lastBoostTime;

    [SerializeField] float fixTransformInterval = 2.0f;

    void Start()
    {
        rbPlayer = GetComponent<Rigidbody>();
    }

    void Update()
    {
        InputBoost();
        RotateShip();
        AbductionBeam();
        FixTransform();
    }

    void AbductionBeam()
    {
        RaycastHit hit;
        Ray beamRay = new Ray(beamOrigin.transform.position, -beamOrigin.transform.up);

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 stationaryPos = transform.position;
            Physics.Raycast(beamRay, out hit, beamDist);
            Debug.DrawRay(beamOrigin.transform.position, -beamOrigin.transform.up * beamDist, Color.red, beamDist);

            if (!beamParticles.isPlaying)
            {
                beamParticles.Play();
            }
            
            if(hit.collider.CompareTag("Cow"))
            {
                rbPlayer.useGravity = false;

                Rigidbody rbCow = hit.rigidbody;
                rbCow.AddRelativeForce(Vector3.up * beamMultiplier * Time.deltaTime, ForceMode.Force);
            }
            else { return; }
        }
        else
        {
            beamParticles.Stop();
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
            if (!mainBoostParticles.isPlaying)
            {
                mainBoostParticles.Play();
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            float boostInterval = Time.time - lastBoostTime;

            if (boostInterval <= doubleBoostInterval)
            {
                if (!dashParticles.isPlaying)
                {
                    dashParticles.Play();
                }
                StartCoroutine(DoubleBoost());
            }
            else
            {
                dashParticles.Stop();
            }
            lastBoostTime = Time.time;
        }
        else
        {
            mainBoostParticles.Stop();
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

    void FixTransform()
    {
        if (transform.position.z != 0)
        {
            FixPosition();
        }
        if (transform.rotation.x != 0 || transform.rotation.y != 0)
        {
            StartCoroutine(FixRotation());
        }
    }

    void FixPosition()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 0);
    }

    IEnumerator FixRotation()
    {
        float time = 0.0f;

        while (time < 1.0f)
        {
            transform.rotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(0, 0, transform.rotation.z), time);
            time += fixTransformInterval * Time.deltaTime;
            yield return null;
        }
    }
}
