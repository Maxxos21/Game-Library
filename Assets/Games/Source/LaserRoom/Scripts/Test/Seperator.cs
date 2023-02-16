using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seperator : MonoBehaviour
{
    [SerializeField] private GameObject laser;
    [SerializeField] public bool isActivated;

    public void Activate(Vector3 direction)
    {
        if (isActivated)
        {
            Debug.Log("Activated");
            
            laser.SetActive(true);

            // Set the laser's rotation to match the outgoing direction, only around the z-axis
            Quaternion laserRotation = Quaternion.LookRotation(direction, Vector3.back);
            laser.transform.rotation = Quaternion.Euler(0f, 0f, laserRotation.eulerAngles.z);
        }
        else
        {
            Debug.Log("Deactivated");
            laser.SetActive(false);
        }
    }
}
