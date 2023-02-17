using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Seperator : MonoBehaviour
{
    [SerializeField] private GameObject laser;

    public void Separate(Vector3 direction)
    {
        laser.transform.rotation = Quaternion.LookRotation(direction);
    }
}