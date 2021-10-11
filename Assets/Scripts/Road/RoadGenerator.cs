using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadGenerator : MonoBehaviour
{
    public List<GameObject> ReadyRoad = new List<GameObject>();
    public GameObject[] Road;
    public bool[] roadNumbers;

    public int currentRoadLength = 0;
    public float distanceBetweenRoads = 10;
    public float speedRoad = 5;
    public float maximumPositionZ = -15;

    public Vector3 waitingZona = new Vector3(-40, 0, 0);

    public int maximumRoadLength = 0;
    private int currentRoadNumber = -1;
    private int lastRoadNumber = -1;

    public string roadGenerationStatus = "Generation";

    private void FixedUpdate()
    {
        if(roadGenerationStatus == "Generation")
        {
            currentRoadNumber = Random.Range(0, Road.Length);

            if (currentRoadLength != lastRoadNumber)
            {
                if (currentRoadNumber < Road.Length / 2)
                {
                    if (roadNumbers[currentRoadNumber] != true)
                    {
                        if (lastRoadNumber != (Road.Length / 2) + currentRoadNumber)
                            RoadCreation();
                        else if (lastRoadNumber == (Road.Length / 2) + currentRoadNumber && currentRoadLength == Road.Length)
                            RoadCreation();
                    }
                }
                else if (currentRoadNumber >= Road.Length / 2)
                {
                    if (roadNumbers[currentRoadNumber] != true)
                    {
                        if (lastRoadNumber != currentRoadNumber - (Road.Length / 2))
                            RoadCreation();
                        else if(lastRoadNumber == currentRoadNumber - (Road.Length/2) && currentRoadLength == Road.Length - 1)
                            RoadCreation();
                    }
                }
            }
            MovingRoad();

            if (ReadyRoad.Count != 0)
                RemovingRoad();
        }
    }
    private void MovingRoad()
    {
        foreach (GameObject readyRoad in ReadyRoad)
            readyRoad.transform.localPosition -= new Vector3(speedRoad * Time.fixedDeltaTime, 0f, 0f);
    }

    private void RemovingRoad()
    {
        if (ReadyRoad[0].transform.localPosition.x < maximumPositionZ)
        {
            int i = 0;
            i = ReadyRoad[0].GetComponent<Road>().number;
            roadNumbers[i] = false;
            ReadyRoad[0].transform.localPosition = waitingZona;
            ReadyRoad.RemoveAt(0);
            currentRoadLength--;
        }
    }

    private void RoadCreation()
    {
        if (ReadyRoad.Count > 0)
            Road[currentRoadNumber].transform.localPosition = ReadyRoad[ReadyRoad.Count - 1].transform.position + new Vector3(distanceBetweenRoads, 0f, 0f);
        else if(ReadyRoad.Count == 0)
            Road[currentRoadNumber].transform.localPosition = new Vector3(0f, 0f, 0f);

        Road[currentRoadNumber].GetComponent<Road>().number = currentRoadNumber;
        roadNumbers[currentRoadNumber] = true;
        lastRoadNumber = currentRoadNumber;
        ReadyRoad.Add(Road[currentRoadNumber]);
        currentRoadLength++;

        // afk 15 min, разберись пока с векторами

    }
}
