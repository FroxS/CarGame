using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(InputMenager))]
[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public InputMenager im;
    public List<WheelCollider> throttleWeels;
    public List<GameObject> steeringWeels;
    public List<GameObject> meshes;
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

        foreach (GameObject wheel in steeringWeels)
        {
            Quaternion initialParentRotation = wheel.transform.parent.rotation;
            wheel.GetComponent<WheelCollider>().steerAngle = maxTurn * im.steer;

            // Zapisz pierwotn¹ rotacjê obiektu nadrzêdnego na starcie
            


            // Oblicz now¹ rotacjê tylko dla osi Y, pozostawiaj¹c pozosta³e bez zmian
            Quaternion newRotation = Quaternion.Euler(0, im.steer * maxTurn, 0);

            // Kombinuj now¹ rotacjê z pierwotn¹ rotacj¹ obiektu nadrzêdnego
            wheel.transform.rotation = newRotation * initialParentRotation;
            //wheel.transform.localEulerAngles = new Vector3(wheel.transform.localEulerAngles.x,im.steer * maxTurn, wheel.transform.localEulerAngles.z);
        }

        foreach (GameObject mesh in meshes)
        {
            float rotationValue = rb.velocity.magnitude / (2 * Mathf.PI * 0.33f);

            if(mesh?.tag?.Length == 3)
            {
                if (mesh.tag[2] == 'R')
                    mesh.transform.Rotate(rb.velocity.magnitude * ((transform.InverseTransformDirection(rb.velocity).z >= 0 ? 1f : -1f) * -1f) / (2 * Mathf.PI * 0.33f), 0f, 0f);
                else
                    mesh.transform.Rotate(rb.velocity.magnitude * ((transform.InverseTransformDirection(rb.velocity).z >= 0 ? 1f : -1f) * 1f) / (2 * Mathf.PI * 0.33f), 0f, 0f);
            }

            
        }
    }

    
}
