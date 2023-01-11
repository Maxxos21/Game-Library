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

        // Clamp to 6x6 Grid
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -7.5f, 7.5f), 0, Mathf.Clamp(transform.position.z, -7.5f, 7.5f));
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            transform.Rotate(0, 90, 0);
        }
    }

}
