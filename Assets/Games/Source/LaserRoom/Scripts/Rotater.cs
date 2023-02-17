using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotater : MonoBehaviour
{
    LaserLevelEditor laserLevelEditor;

    void Awake()
    {
        laserLevelEditor = GetComponentInParent<LaserLevelEditor>();
    }

    private void OnMouseOver()
    {
        if (laserLevelEditor.isRotatable == false) return;
        
        if (Input.GetMouseButtonDown(1))
        {
            transform.Rotate(0, 90, 0);
        }
    }
}
