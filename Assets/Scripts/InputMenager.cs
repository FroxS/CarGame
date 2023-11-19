using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMenager : MonoBehaviour
{
    public float throttles;
    public float steer;



    // Update is called once per frame
    void Update()
    {
        // Pobierz input od gracza
        //float horizontalInput = Input.GetAxis("Horizontal");
        throttles = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");

        // Poruszanie do przodu i do ty³u
        //transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);

        // Obracanie w lewo i w prawo
        //transform.Rotate(Vector3.up * horizontalInput * rotationSpeed * Time.deltaTime);
    }

    
}
