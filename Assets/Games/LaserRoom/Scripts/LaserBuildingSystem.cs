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

    public void SpawnMirror()
    {
        InitializeWithObject(mirrorPrefab, Vector3.zero, Quaternion.identity);
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
    #endregion
}
