using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pipePrefab;
    private List<int> solution = new List<int>();
    private List<int> currentArrangement = new List<int>();

    private void Start()
    {
        GetPipeRotation();
        RandomizePipeRotation();
    }

    private void RandomizePipeRotation()
    {
        foreach (GameObject pipe in pipePrefab)
        {
            PipeLevelCreator pipeLevelCreator = pipe.GetComponent<PipeLevelCreator>();
            int rotation = Random.Range(0, 4);
            pipeLevelCreator.rotation = rotation;
            pipeLevelCreator.UpdateRotation();
        }
    }

    private void GetPipeRotation()
    {
        foreach (GameObject pipe in pipePrefab)
        {
            PipeLevelCreator pipeLevelCreator = pipe.GetComponent<PipeLevelCreator>();
            int rotation = pipeLevelCreator.rotation;
            solution.Add(rotation);
        }

        string solutionString = string.Join(",", solution.ToArray());
        Debug.Log(solutionString);
    }
}