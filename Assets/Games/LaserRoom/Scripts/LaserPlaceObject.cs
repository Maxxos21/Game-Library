using System;
using UnityEngine;

[Serializable]
public class LaserPlaceObject : MonoBehaviour
{
    [Header("Object Initialization")]
    public bool isMovable = true;
    public GameObject prefab;
    [Range(1,36)] public int objectLocation;
    [Range(1,4)] public int rotation;

    public void Start()
    {
        rotation *= 90;
    }
}
