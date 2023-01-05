using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LaserBuildingSystem : MonoBehaviour
{
    public static LaserBuildingSystem current;
    public GridLayout gridLayout;
    private Grid grid;
    [SerializeField] private Tilemap mainTilemap;
    [SerializeField] private TileBase whiteTile;
    public GameObject mirrorPrefab;
    private LaserPlaceableObject objectToPlace;

    #region Unity Methods

    private void Awake()
    {
        current = this;
        grid = gridLayout.GetComponent<Grid>();
    }

    // spawn 3 mirrors at the start of the game at the center of the grid
    private void Start()
    {
        Vector3Int cellPosition = gridLayout.WorldToCell(Vector3.zero);
        Vector3 position = grid.GetCellCenterWorld(cellPosition);
        InitializeWithObject(mirrorPrefab, position + new Vector3(-1, 0, -1), Quaternion.identity);
        InitializeWithObject(mirrorPrefab, position + new Vector3(-2, 0, -2), Quaternion.identity);
        InitializeWithObject(mirrorPrefab, position + new Vector3(0, 0, 0), Quaternion.identity);
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

    public Vector3 SnapCoordinateToGrid(Vector3 position)
    {
        Vector3Int cellPosition = gridLayout.WorldToCell(position);
        position = grid.GetCellCenterWorld(cellPosition);
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
    // {
    //     Vector3 position = SnapCoordinateToGrid(Vector3.zero);

    //     GameObject obj = Instantiate(prefab, position, Quaternion.identity);
    //     objectToPlace = obj.GetComponent<LaserPlaceableObject>();
    //     obj.AddComponent<LaserObjectDrag>();

    // }
    
    #endregion
}
