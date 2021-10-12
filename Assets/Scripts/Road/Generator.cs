using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private Transform level;
    private void Awake()
    {
        SpawnRoadPart(new Vector3(20, 0));
        SpawnRoadPart(new Vector3(20, 0) + new Vector3(20,0));
        SpawnRoadPart(new Vector3(20, 0) + new Vector3(20,0)); //x - 0.34 позиция примерной равности блоков
    }

    private void SpawnRoadPart(Vector3 spawnPosition)
    {
        Instantiate(level, spawnPosition, Quaternion.identity);
    }

}
