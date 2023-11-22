using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameMenager : MonoBehaviour
{
    public List<GameObject> points;

    public List<GameObject> finishedpoints;
    public GameObject actualPoint;

    public bool isFinish;

    void Start()
    {
        foreach(var point in points)
        {
            point.SetActive(false);
        }
        if(points.Count > 0)
        {
            points[0].SetActive(true);
            actualPoint = points[0];
        }
        finishedpoints = new List<GameObject>();
        isFinish = false;

    }

    public void PastPoint(GameObject point)
    {
        
        if (actualPoint == point)
        {
            point.SetActive(false);
            finishedpoints.Add(point);
            if (finishedpoints.Count == points.Count)
            {
                Debug.Log("Finish");
                isFinish = true;
                return;
            }
            int newPoint = finishedpoints.Count;
            
            points[newPoint].SetActive(true);
            Debug.Log($"new point id: {newPoint}, New point {points[newPoint].name} -> {points[newPoint].active}");
            actualPoint = points[newPoint];
            //return;
        }

    }
}
