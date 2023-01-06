using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPlaceableObject : MonoBehaviour
{
    public float rotateSpeed = 1.0f;
    public bool isRotating = false;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject == gameObject)
                {
                    transform.Rotate(0, 90, 0);
                }
            }
        }
    }
}