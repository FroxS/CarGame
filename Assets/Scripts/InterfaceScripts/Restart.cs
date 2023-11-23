using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public GameMenager gm;

    // Update is called once per frame
    void Update()
    {
        if (gm.isFinish)
        {
            GetComponent<TextMeshProUGUI>().text = $"koniec gry naci�nij 'R', aby zrestarowa�";

            if (Input.GetKeyDown(KeyCode.R))
            {
                gm.Restart();
                GetComponent<TextMeshProUGUI>().text = "";
            }
        }
        
    }
}
