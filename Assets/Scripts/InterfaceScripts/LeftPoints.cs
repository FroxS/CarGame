using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeftPoints : MonoBehaviour
{
    public GameMenager gm;

    // Update is called once per frame
    void Update()
    {
        if(!gm.isFinish)
            GetComponent<TextMeshProUGUI>().text = $"{gm.allPoints}/{gm.leftPoints}";
    }
}
