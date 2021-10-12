using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private List<GameObject> ReadyRoad = new List<GameObject>();

    [Header("Все участки дорог")]
    public GameObject[] Road;
    public bool[] roadNumber;

    [Header("Текущая длина дороги")]
    public int currentRoadLength = 0;

    [Header("Максимальная длинна дороги")]
    public int maximumRoadLength = 6;

    [Header("Дистанция между дорогами")]
    public float distanceBetweenRoads;

    [Header("Скорость дороги")]
    public float speedRoad = 5;

    [Header("Крайняя точка исчезновения")]
    public float maximumPositionX = -15;

    [Header("Зона ожидания")]
    public Vector3 waitingZona = new Vector3(-40, 0, 0);

    private int currentRoadNumber = -1;
    private int lastRoadNumber = -1;

    [Header("Стутас генерации")]
    public string roadGenerationStatus = "Generation";

    private void FixedUpdate()
    {
        if (roadGenerationStatus == "Generation")
        {
            if (currentRoadLength != maximumRoadLength)
            {
                currentRoadNumber = Random.Range(0, Road.Length);

                if (currentRoadNumber != lastRoadNumber)
                {
                    if (currentRoadNumber < Road.Length / 2)
                    {
                        if (lastRoadNumber != (Road.Length / 2) + currentRoadNumber)
                        {
                            RoadCreation();
                        }
                        else if (lastRoadNumber == (Road.Length / 2) + currentRoadNumber && currentRoadLength == Road.Length - 1)
                        {
                            RoadCreation();
                        }
                    }
                }
                else if (currentRoadNumber >= Road.Length / 2)
                {
                    if (roadNumber[currentRoadNumber] != true)
                    {
                        if (lastRoadNumber != currentRoadNumber - (Road.Length / 2))
                        {
                            RoadCreation();
                        }
                        else if (lastRoadNumber == currentRoadNumber - (Road.Length / 2) && currentRoadLength == Road.Length - 1)
                        {
                            RoadCreation();
                        }
                    }
                }
            }

            MovingRoad();

            if (ReadyRoad.Count != 0)
            {
                RemovingRoad();
            }

        }

    }

    private void MovingRoad()
    {
        foreach (GameObject readyRoad in ReadyRoad)
        {
            readyRoad.transform.localPosition -= new Vector3(speedRoad * Time.fixedDeltaTime, 0f, 0f);
        }
    }

    private void RemovingRoad()
    {
        if (ReadyRoad[0].transform.localPosition.x < maximumPositionX)
        {
            int i = ReadyRoad[0].GetComponent<Road>().number;
            roadNumber[i] = false;
            ReadyRoad[0].transform.localPosition = waitingZona;
            ReadyRoad.RemoveAt(0);
            currentRoadLength--;
        }
    }

    private void RoadCreation()
    {
        if (ReadyRoad.Count > 0)
        {
            Road[currentRoadNumber].transform.localPosition = ReadyRoad[ReadyRoad.Count - 1].transform.position + new Vector3(distanceBetweenRoads, 0f, 0f);
        }
        else if (ReadyRoad.Count == 0)
        {
            Road[currentRoadNumber].transform.localPosition = new Vector3(0f, 0f, 0f);
        }

        Road[currentRoadNumber].GetComponent<Road>().number = currentRoadNumber;

        roadNumber[currentRoadNumber] = true;

        lastRoadNumber = currentRoadNumber;

        ReadyRoad.Add(Road[currentRoadNumber]);

        currentRoadLength++;
    }
}
