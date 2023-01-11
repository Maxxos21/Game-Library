using System;
using UnityEngine;

[Serializable]
public class LaserPlaceObject : MonoBehaviour
{
    [Header("Object Initialization")]
    public bool isMovable = true;
    public GameObject prefab;
    [Range(1,36)] public int spawnLocation;
    [Range(1,4)] public int rotation;

    [Header("Object Properties - Live")]
    public Vector3 position;

    public void Start()
    {
        rotation *= 90;
    }
}
