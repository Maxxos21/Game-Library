using System;
using UnityEngine;

[Serializable]
public class LaserPlaceObject : MonoBehaviour
{
    [Header("Object Initialization")]
    public bool isMovable = true;
    public GameObject prefab;
    [Range(1,36)]public int objectLocation;
}
