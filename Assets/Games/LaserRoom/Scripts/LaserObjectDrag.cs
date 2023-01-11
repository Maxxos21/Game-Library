using UnityEngine;
using System.Linq;

public class LaserObjectDrag : MonoBehaviour
{
    private Vector3 offset;

    public LaserBuildingSystem laserBS;

    private void OnMouseDown()
    {
        // Get the current grid cell position of the object
        Vector3Int cellPosition = LaserBuildingSystem.current.gridLayout.WorldToCell(transform.position);
    
        // Find the corresponding obj.key in the objectsToPlace dictionary
        int objKey = (cellPosition.z + 5) * 6 + (cellPosition.x + 5);

        Debug.Log("objKey: " + objKey);
    
        // Remove the obj.key from its current position in the dictionary
        LaserBuildingSystem.current.objectsToPlace.Remove(objKey);
    
        offset = transform.position - LaserBuildingSystem.GetMouseWorldPosition();
    }

    private void OnMouseUp()
    {

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
