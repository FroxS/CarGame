using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public GameMenager gm;

    // Update is called once per frame
    void Update()
    {
        TextMeshProUGUI tmp = GetComponent<TextMeshProUGUI>();
        if(tmp != null)
        {
            string text = "";
            if(gm.prevResult >0)
                text += $"{gm.prevResult.ToString("n2")} s\n";

            text += $"{gm.timer.ToString("n2")}s";
            tmp.SetText(text);
        }

        
    }
}
