using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class LaserBuildingSystem : MonoBehaviour
{
    public static LaserBuildingSystem current;
    public GridLayout gridLayout;
    public Grid grid;
    public Dictionary<int, LaserObjectContainer> objectsToPlace = new Dictionary<int, LaserObjectContainer>();
    public Vector3[] spawnPosition;

    private void Awake()
    {
        current = this;
        grid = gridLayout.GetComponent<Grid>();
    } 

    private void Start()
    {   
        CreateGrid();
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit)) { return hit.point; }
        else { return Vector3Int.zero;}
    }

    public void CreateGrid()
    {
        float gridSize = 7.5f;
        spawnPosition = new Vector3[36];

        for (int i = 5; i >= 0; i--)
        {
            for (int j = 0; j < 6; j++)
            {
                spawnPosition[i * 6 + j] = new Vector3(gridSize - 1.5f - (3f * j), 0f, -gridSize + (3f * i));
            }
        }
    }


    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPosition = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPosition);
        position.y = 0;
        return position;
    }
}

