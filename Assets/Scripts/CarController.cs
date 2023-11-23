using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(InputMenager))]
[RequireComponent(typeof(Rigidbody))]
public class CarController : MonoBehaviour
{
    public InputMenager im;
    public GameMenager gm;
    public List<WheelCollider> throttleWeels;
    public List<GameObject> steeringWeels;
    public List<GameObject> meshes;
    public float strengthCoofficient = 2000f;
    public float maxTurn = 20f;
    public Transform CM;
    public Rigidbody rb;
    private float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        im = GetComponent<InputMenager>();
        rb = GetComponent<Rigidbody>();
        //Debug.LogError($"{gm}");
        //gm = gm.GetComponent<GameMenager>();
        //Debug.LogError($"{gm}");
        if (CM)
        {
            //rb.centerOfMass = CM.position;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Point")
        {
            if(gm==null)
            {
                Debug.LogError($"Za³aduj game menager");
                return;
            }

            gm.PastPoint(other.gameObject);
            return;
        }
            
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckPosition();

        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 10f, 0f);
        }

        foreach (WheelCollider wheel in throttleWeels)
        {
            if(!gm.IsEngingeStarting)
                wheel.motorTorque = strengthCoofficient * Time.deltaTime * im.throttles;
        }

        foreach (GameObject wheel in steeringWeels)
        {
            Quaternion initialParentRotation = wheel.transform.parent.rotation;
            wheel.GetComponent<WheelCollider>().steerAngle = maxTurn * im.steer;
            Quaternion newRotation = Quaternion.Euler(0, im.steer * maxTurn, 0);
            wheel.transform.rotation = newRotation * initialParentRotation;
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

        if (gm.EngineAudio != null && gm.EngineAudio.isPlaying)
        {

            //gm.EngineAudio.volume += im.throttles * speed * Time.deltaTime;
            //Debug.LogWarning($"Test: {gm.EngineAudio.volume}");
            //gm.EngineAudio.volume = Mathf.Clamp(gm.EngineAudio.volume, 0f, 1f);
            float rigidBodyMangintude = rb.velocity.magnitude;
            float pitch = mapValue(rigidBodyMangintude, 0f, 10f, 1f, 1.5f);
            Debug.LogWarning($"Pith: {pitch}");
            gm.EngineAudio.pitch = pitch;
        }

    }

    float mapValue(float mainValue, float inValueMin, float inValueMax, float outValueMin, float outValueMax)
    {
        return (mainValue - inValueMin) * (outValueMax - outValueMin) / (inValueMax - inValueMin) + outValueMin;
    }

    public void CheckPosition()
    {
        Vector3 pozycjaObiektu = transform.position;
        // SprawdŸ, czy pozycja obiektu znajduje siê na terenie
        if (pozycjaObiektu.y <= -20)
            gm.Restart();
    }
}
