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
        throttles = Input.GetAxis("Vertical");
        steer = Input.GetAxis("Horizontal");
    }
    
}
