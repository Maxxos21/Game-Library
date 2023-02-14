using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class LaserObjectDrag : MonoBehaviour
{
    private Vector3 offset;
    LaserObjectContainer laserObjectContainer;

    void Awake()
    {
        laserObjectContainer = GetComponentInParent<LaserObjectContainer>();
    }

    private void OnMouseDown()
    {
        if (laserObjectContainer.isMovable == false) return;

        offset = transform.position - LaserBuildingSystem.GetMouseWorldPosition();

        if (LaserBuildingSystem.current.spawnPosition.Contains(transform.position))
        {
            LaserBuildingSystem.current.spawnPosition = LaserBuildingSystem.current.spawnPosition.Where(val => val != transform.position).ToArray();
        }
    }

    private void OnMouseUp()
    {
        if (!LaserBuildingSystem.current.spawnPosition.Contains(transform.position))
        {
            LaserBuildingSystem.current.spawnPosition = LaserBuildingSystem.current.spawnPosition.Concat(new Vector3[] { transform.position }).ToArray();
        }
    }

    private void OnMouseDrag()
    {
        if (laserObjectContainer.isMovable == false) return;
        
        Vector3 pos = LaserBuildingSystem.GetMouseWorldPosition() + offset;
        Vector3 newPos = LaserBuildingSystem.current.SnapCoordinateToGrid(pos);
        Vector3 raycastDirection = new Vector3(0, 1, 0);
        float raycastDistance = 1.5f;

        if(newPos.x >= -7.5f && newPos.x <= 7.5f && newPos.z >= -7.5f && newPos.z <= 7.5f && !LaserBuildingSystem.current.spawnPosition.Contains(newPos) && !Physics.Raycast(newPos, raycastDirection, raycastDistance))
        {
            LaserBuildingSystem.current.spawnPosition = LaserBuildingSystem.current.spawnPosition.Where(val => val != transform.position).ToArray();
            LaserBuildingSystem.current.spawnPosition = LaserBuildingSystem.current.spawnPosition.Concat(new Vector3[] { newPos }).ToArray();
            transform.position = newPos;
        } 
    }
}
