using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private Transform level;
    private void Awake()
    {
        
        Instantiate(level, new Vector3(20,(float) 0, 0), Quaternion.identity);
    }
}
