using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;
using static UnityEngine.ParticleSystem;

public class GameMenager : MonoBehaviour
{
    public float prevResult = 0;
    public GameObject Road;
    public bool isFinish = false;
    public float timer = 0;
    public int allPoints = 0;
    public int leftPoints = 0;
    public GameObject car;
    public Terrain terrain;
    public AudioSource StartEngineAudio;
    public AudioSource EngineAudio;
    public bool IsEngingeStarting = true;

    private List<GameObject> points;
    private List<GameObject> finishedpoints;
    private GameObject actualPoint;
    

    private bool gameStarted = false;

    void Start()
    {
        points = new List<GameObject>();
        if (Road != null)
        {
            Transform roadTransform = Road.transform;

            for (int i = 0; i < roadTransform.childCount; i++)
            {
                Transform child = roadTransform.GetChild(i);

                if(child.gameObject.tag == "Point")
                {
                    points.Add(child.gameObject);
                    
                }
                    
            }
            Restart();
        }
        else
        {
            Debug.LogError("Pole Road nie zosta³o przypisane!");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (gameStarted)
        {
            timer += Time.deltaTime;
        }
        else
        {
            if (!isFinish)
            {
                timer = 0;
            }
        }
    }

    public void PastPoint(GameObject point)
    {
        if (actualPoint == point)
        {
            point.SetActive(false);
            finishedpoints.Add(point);
            leftPoints++;
            if (finishedpoints.Count == points.Count)
            {
                gameStarted = false;
                isFinish = true;
                prevResult = timer;
                timer = 0;
                return;
            }
            int newPoint = finishedpoints.Count;
            
            points[newPoint].SetActive(true);
            actualPoint = points[newPoint];
            gameStarted = true;
        }
    }

    public void Restart()
    {
        
        if (EngineAudio != null && EngineAudio.isPlaying)
            EngineAudio.Stop();

        foreach (GameObject point in points) 
        {
            point.SetActive(false);
        }
        points[0].SetActive(true);
        actualPoint = points[0];
        leftPoints = 0;
        allPoints = points.Count;
        finishedpoints = new List<GameObject>();
        gameStarted = false;
        isFinish = false;

        car.transform.position = new Vector3(200f, 2f, 160f);
        car.transform.rotation = new Quaternion(
            car.transform.rotation.x,
            160f,
            car.transform.rotation.z,
            car.transform.rotation.w
            );
        car.GetComponent<Rigidbody>().velocity = Vector3.zero;
        car.transform.localScale = new Vector3(3f, 3f, 3f);
        StartCoroutine(StartEngine());
    }

    private IEnumerator StartEngine()
    {
        if (StartEngineAudio != null)
        {
            IsEngingeStarting = true;
            StartEngineAudio.Play();
            while (StartEngineAudio.isPlaying)
                yield return null;
            
            IsEngingeStarting = false;
            if (EngineAudio != null)
            {
                EngineAudio.loop = true;
                EngineAudio.Play();
            }
        }
        else
        {
            IsEngingeStarting = false;
        }
    }



    public float GetVelocity()
    {
        if(car != null)
        {
            Rigidbody rb = car.GetComponent<Rigidbody>();
            if(rb != null)
            {
                return rb.velocity.magnitude * 3.6f * Time.deltaTime * 10f;
            }
            else
            {
                Debug.Log("Nie przypisano righbody");
            }
        }
        else
        {
            Debug.Log("Nie przypisano auta");
        }
        return 0f;
    }
}
