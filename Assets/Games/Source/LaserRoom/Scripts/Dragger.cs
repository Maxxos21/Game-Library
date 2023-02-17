using UnityEngine;
using System.Linq;
using UnityEngine.EventSystems;

public class Dragger : MonoBehaviour
{
    private Vector3 offset;
    LaserLevelEditor laserLevelEditor;

    void Awake()
    {
        laserLevelEditor = GetComponentInParent<LaserLevelEditor>();
    }

    private void OnMouseDown()
    {
        if (laserLevelEditor.isMovable == false) return;

        offset = transform.position - GridSetup.GetMouseWorldPosition();

        if (GridSetup.current.spawnPosition.Contains(transform.position))
        {
            GridSetup.current.spawnPosition = GridSetup.current.spawnPosition.Where(val => val != transform.position).ToArray();
        }
    }

    private void OnMouseUp()
    {
        if (!GridSetup.current.spawnPosition.Contains(transform.position))
        {
            GridSetup.current.spawnPosition = GridSetup.current.spawnPosition.Concat(new Vector3[] { transform.position }).ToArray();
        }
    }

    private void OnMouseDrag()
    {
        if (laserLevelEditor.isMovable == false) return;
        
        Vector3 pos = GridSetup.GetMouseWorldPosition() + offset;
        Vector3 newPos = GridSetup.current.SnapCoordinateToGrid(pos);
        Vector3 raycastDirection = new Vector3(0, 1, 0);
        float raycastDistance = 1.5f;

        if(newPos.x >= -7.5f && newPos.x <= 7.5f && newPos.z >= -7.5f && newPos.z <= 7.5f && !GridSetup.current.spawnPosition.Contains(newPos) && !Physics.Raycast(newPos, raycastDirection, raycastDistance))
        {
            GridSetup.current.spawnPosition = GridSetup.current.spawnPosition.Where(val => val != transform.position).ToArray();
            GridSetup.current.spawnPosition = GridSetup.current.spawnPosition.Concat(new Vector3[] { newPos }).ToArray();
            transform.position = newPos;
        } 
    }
}
