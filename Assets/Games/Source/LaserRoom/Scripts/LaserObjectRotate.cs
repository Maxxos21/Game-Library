using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObjectRotate : MonoBehaviour
{
    LaserObjectContainer laserObjectContainer;

    void Awake()
    {
        laserObjectContainer = GetComponentInParent<LaserObjectContainer>();
    }

    private void OnMouseOver()
    {
        if (laserObjectContainer.isRotatable == false) return;
        
        if (Input.GetMouseButtonDown(1))
        {
            transform.Rotate(0, -90, 0);
        }
    }
}
