using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputMenager))]
[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public InputMenager im;
    public List<WheelCollider> throttleWeels;
    public List<WheelCollider> steeringWeels;
    public float strengthCoofficient = 2000f;
    public float maxTurn = 20f;
    public Transform CM;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        im = GetComponent<InputMenager>();
        rb = GetComponent<Rigidbody>();

        if (CM)
        {
            //rb.centerOfMass = CM.position;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach(WheelCollider wheel in throttleWeels)
        {
            wheel.motorTorque = strengthCoofficient * Time.deltaTime * im.throttles;
        }

        foreach (WheelCollider wheel in steeringWeels)
        {
            wheel.steerAngle = maxTurn * im.steer;
        }
    }
}
