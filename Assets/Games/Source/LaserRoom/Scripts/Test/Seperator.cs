using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Seperator : MonoBehaviour
{
    [SerializeField] private GameObject laser;
    [SerializeField] private bool isSeperator;
    public void Separate(RaycastHit hit)
    {
        Vector3 outgoingDirection = hit.point - transform.position;
        laser.transform.rotation = Quaternion.LookRotation(outgoingDirection);
    }
}
