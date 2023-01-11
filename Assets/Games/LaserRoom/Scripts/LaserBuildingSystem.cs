using UnityEngine;
using System.Collections.Generic;

public class LaserBuildingSystem : MonoBehaviour
{
    public static               LaserBuildingSystem             current;
    public                      GridLayout                      gridLayout;
    public                      Grid                            grid;
    public                      GameObject                      doubleMirrorPrefab;
    public                      GameObject                      mirrorPrefab;
    private                     LaserPlaceableObject            objectToPlace;
    [SerializeField] private    int[]                           spawns;
    private                     Vector3[]                       spawnPosition;
    public                     List<int>                       placedObjects = new List<int>();

    #region Unity Methods

    private void Awake()
    {
        current = this;
        grid = gridLayout.GetComponent<Grid>();
        CreateGrid();

    }

    private void Start()
    {
        foreach (int spawn in spawns)
        {
            SpawnMirrorAtIndex(spawn);
        }
    }

    #endregion 
    #region Utils

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point;
        }
        else
        {
            return Vector3Int.zero;
        }
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

    #endregion
    #region Building Placement
    
    public void InitializeWithObject(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject obj = Instantiate(prefab, position, rotation);
        objectToPlace = obj.GetComponent<LaserPlaceableObject>();
        obj.AddComponent<LaserObjectDrag>();
    }

    public void SpawnMirrorAtIndex(int index)
    {
        Vector3 position = spawnPosition[index - 1];
        position = SnapCoordinateToGrid(position);
        InitializeWithObject(mirrorPrefab, position, Quaternion.identity);

        placedObjects.Add(index);
    }

    #endregion
}

public class CountainerForObjects : MonoBehaviour
{
        private Vector3[]   spawnPosition;
        private Vector3[]   spawnRotation;
}
