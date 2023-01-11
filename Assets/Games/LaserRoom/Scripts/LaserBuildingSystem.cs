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
    public Dictionary<int, LaserPlaceObject> objectsToPlace = new Dictionary<int, LaserPlaceObject>();
    public Vector3[] spawnPosition;


    private void Awake()
    {
        current = this;
        grid = gridLayout.GetComponent<Grid>();
        CreateGrid();

        // Get all objects to place
        GameObject objManager = GameObject.Find("Object_Manager");

        foreach (Transform child in objManager.transform)
        {
            LaserPlaceObject obj = child.GetComponent<LaserPlaceObject>();
            objectsToPlace.Add(obj.objectLocation, obj);
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
        foreach (KeyValuePair<int, LaserPlaceObject> obj in objectsToPlace)
        {
            Vector3 position = spawnPosition[obj.Key - 1];
            position = SnapCoordinateToGrid(position);

            GameObject newObj = Instantiate(obj.Value.prefab, position, Quaternion.identity);
            newObj.transform.parent = objectsToPlace[obj.Key].transform;

            if (obj.Value.isMovable)
            {
                newObj.AddComponent<LaserObjectDrag>();
            }
        }
    }

}

