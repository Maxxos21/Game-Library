using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserObjectDrag : MonoBehaviour
{
    private Vector3 offset;

    private void OnMouseDown()
    {
        offset = transform.position - LaserBuildingSystem.GetMouseWorldPosition();
    }

    private void OnMouseDrag()
    {
        Vector3 pos = LaserBuildingSystem.GetMouseWorldPosition() + offset;
        transform.position = LaserBuildingSystem.current.SnapCoordinateToGrid(pos);
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);

        // clamp th placement in a grid of 6x6, scale one tile is 3
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), 0, Mathf.Clamp(transform.position.z, -7.5f, 7.5f));

    }
}
