using System;
using UnityEngine;

[Serializable]
public class LaserObjectContainer : MonoBehaviour
{
    [Header("Object Initialization")]
    public bool isMovable = true;
    public GameObject prefab;
    [Range(1,36)] public int objectLocation;
    enum ObjectRotation { Zero, Ninety, OneEighty, TwoSeventy };
    [Header("Object Rotation")]
    [SerializeField] private ObjectRotation objectRotation = ObjectRotation.Zero;
    public int rotation
    {
        get { return (int)objectRotation * 90; }
        set { objectRotation = (ObjectRotation)value; }
    }

}
