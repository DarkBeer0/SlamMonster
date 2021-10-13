using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Generator : MonoBehaviour
{
    [SerializeField] private Transform level;
    [SerializeField] private Transform trap;
    public Transform DefaultFloor;
    public Transform DefaultRoof;

    private static DateTime lastUpdate;

    private static List<Transform> roadList = new List<Transform>();
    private static List<Transform> trapList = new List<Transform>();

    private float floorY = 0, roofY = 0;

    private void Awake()
    {
        if (DefaultFloor != null)
            floorY = DefaultFloor.position.y + DefaultFloor.localScale.y / 2;

        if (DefaultRoof != null)
            roofY = DefaultRoof.position.y - DefaultRoof.localScale.y / 2;

        SpawnRoadPart(new Vector3(20, 0));
        SpawnRoadPart(new Vector3(40, 0));
        SpawnRoadPart(new Vector3(60, 0)); //y - 0.34 позиция примерной равности блоков
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
            trapList.Add(Instantiate(trap, new Vector3(bufferRoad.position.x, floorY, bufferRoad.position.z), Quaternion.identity));
        }

        Debug.Log("Update");

        lastUpdate = DateTime.Now;

    }

}
