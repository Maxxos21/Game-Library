using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private GameObject[] pipePrefab;
    private List<int> solution = new List<int>();
    private List<int> currentArrangement = new List<int>();
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material correctMaterial;
    [SerializeField] private GameObject winPipe;

    private void Start()
    {
        GetInitailPipeRotations();
        RandomizePipeRotation();
        GetPipeRotations();
    }

    private void RandomizePipeRotation()
    {
        foreach (GameObject pipe in pipePrefab)
        {
            PipeLevelCreator pipeLevelCreator = pipe.GetComponent<PipeLevelCreator>();
            if (pipeLevelCreator.activeOption == PipeLevelCreator.ChildActivationEnum.Straight)
            {
                pipeLevelCreator.rotation = Random.Range(0, 2);
                pipeLevelCreator.UpdateRotation();
            }
            else
            {
                pipeLevelCreator.rotation = Random.Range(0, 4);
                pipeLevelCreator.UpdateRotation();
            }
        }
    }

    private void GetInitailPipeRotations()
    {
        foreach (GameObject pipe in pipePrefab)
        {
            PipeLevelCreator pipeLevelCreator = pipe.GetComponent<PipeLevelCreator>();
            int rotation = pipeLevelCreator.rotation;
            solution.Add(rotation);
        }

        string solutionString= string.Join(",", solution.ToArray());
        Debug.Log(solutionString + " : " + solutionString.Length);
    }

    public void GetPipeRotations()
    {
        currentArrangement.Clear();

        foreach (GameObject pipe in pipePrefab)
        {
            PipeLevelCreator pipeLevelCreator = pipe.GetComponent<PipeLevelCreator>();
            int rotation = pipeLevelCreator.rotation;
            currentArrangement.Add(rotation);
        }

        string currentArrangementString = string.Join(",", currentArrangement.ToArray());
        Debug.Log(currentArrangementString + " : " + currentArrangementString.Length);

        CheckSolution();
    }

    private void CheckSolution()
    {
        for (int i = 0; i < solution.Count; i++)
        {
            if (solution[i] != currentArrangement[i])
            {
                pipePrefab[i].GetComponentInChildren<Renderer>().material = defaultMaterial;
                return;
            }

            if (i == solution.Count - 1)
            {
                Debug.Log("You Win!");

                foreach (GameObject pipe in pipePrefab)
                {
                    pipe.GetComponentInChildren<Renderer>().material = correctMaterial;
                    winPipe.GetComponent<Renderer>().material = correctMaterial;
                }
            }
        }


    }
}