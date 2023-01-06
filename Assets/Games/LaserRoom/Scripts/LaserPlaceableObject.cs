using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPlaceableObject : MonoBehaviour
{
    private void Update()
    {
        // only rotate if the mouse is over the object
        if (Input.GetMouseButtonDown(1))
        {
            // Check if the mouse is over the object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object hit by the ray is the same as this game object
                if (hit.transform.gameObject == gameObject)
                {
                    // Rotate the object 90 degrees around the y-axis
                    transform.Rotate(0, 90, 0);
                }
            }
        }
    }
}