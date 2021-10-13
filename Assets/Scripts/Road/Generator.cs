using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Generator : MonoBehaviour
{
    [SerializeField] private Transform level;
    private static DateTime lastUpdate;
    private static List<Transform> roadList = new List<Transform>();
    private void Awake()
    {
        SpawnRoadPart(new Vector3(20, 0));
        SpawnRoadPart(new Vector3(20, 0) + new Vector3(20,0));
        SpawnRoadPart(new Vector3(40, 0) + new Vector3(20,0)); //y - 0.34 позиция примерной равности блоков
    }

    private void SpawnRoadPart(Vector3 spawnPosition)
    {
        roadList.Add(Instantiate(level, spawnPosition, Quaternion.identity));
        
    }

    public void Update()
    {
        if (DateTime.Now.Subtract(lastUpdate).TotalMilliseconds < 2000)
            return;
        #region MoveRoad

        Transform lastRoadObject = roadList.Last();
        roadList.First().position = new Vector3(lastRoadObject.position.x + 20, lastRoadObject.position.y, lastRoadObject.position.z);
        Transform bufferRoad = roadList.First();

        roadList.Add(roadList.First());
        roadList.Remove(bufferRoad);

        #endregion

        // Last road pos = bufferRoad


        if (Random.Range(0, 5) < 2)
        {
            Debug.Log("Monstr at: " + bufferRoad.position);
            // TODO
        }

        Debug.Log("Update");

        lastUpdate = DateTime.Now;

    }

}
