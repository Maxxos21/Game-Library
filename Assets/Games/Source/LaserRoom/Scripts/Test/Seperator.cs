using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Seperator : MonoBehaviour
{
    [SerializeField] private GameObject laser;
    public void Separate(Vector3 direction)
    {
        // Calculate the angle between the current laser direction and the desired direction
        float angle = Vector3.SignedAngle(laser.transform.forward, direction, Vector3.up);

        // Rotate the laser object by the calculated angle
        laser.transform.Rotate(Vector3.up, angle, Space.Self);
    }
}