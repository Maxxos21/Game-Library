using UnityEngine;
using System.Collections.Generic;
using System;

public class LaserBuildingSystem : MonoBehaviour
{

    // Singleton
    public static LaserBuildingSystem current;


    // Grid Variables
    [HideInInspector]
    public GridLayout gridLayout;
    [HideInInspector]
    public Grid grid;


    // Spawn Variables
    private List<int> placedObjects = new List<int>();
    private List<LaserPlaceObject> objectsToPlace = new List<LaserPlaceObject>();
    private Dictionary<int, GameObject> testPlacedObject = new Dictionary<int, GameObject>();
    private Vector3[] spawnPosition;


    private void Awake()
    {
        current = this;
        grid = gridLayout.GetComponent<Grid>();
        CreateGrid();

        // Get all objects to place
        GameObject objManager = GameObject.Find("Object_Manager");
        foreach (Transform child in objManager.transform)
        {
            objectsToPlace.Add(child.GetComponent<LaserPlaceObject>());
        }

    }

    private void Start()
    {
        SpawnItems();
    }

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

    public void SpawnItems()
    {
        foreach (LaserPlaceObject obj in objectsToPlace)
        {
            Vector3 position = spawnPosition[obj.spawnLocation - 1];
            position = SnapCoordinateToGrid(position);
            GameObject objToPlace = Instantiate(obj.prefab, position, Quaternion.Euler(0, obj.rotation, 0));

            if (obj.isMovable)
            {
                objToPlace.AddComponent<LaserObjectDrag>();
            }

            placedObjects.Add(obj.spawnLocation);
        }

    }

}

