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

        // cant place an object outisde of a grid of 5x5
        if (Mathf.Abs(transform.position.x) > 5 || Mathf.Abs(transform.position.z) > 5)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -5, 5), 0, Mathf.Clamp(transform.position.z, -5, 5));
        }
    }
}
