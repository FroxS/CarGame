using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Console : MonoBehaviour
{
    public GameMenager gm;

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
        if(tmp != null)
        {
            tmp.text = $"{gm.GetVelocity().ToString("n2")} km/h";
        }
        
    }
}
