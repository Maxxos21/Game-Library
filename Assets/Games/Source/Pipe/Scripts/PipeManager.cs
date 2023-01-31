using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private GameObject gamePipe;
    [SerializeField] private List<GameObject> pipePrefab = new List<GameObject>();
    private List<int> solution = new List<int>();
    private List<int> currentArrangement = new List<int>();
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material correctMaterial;
    [SerializeField] private GameObject winPipe;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        for (int i = 0; i < gamePipe.transform.childCount; i++)
        {
            Transform child = gamePipe.transform.GetChild(i);
            pipePrefab.Add(child.gameObject);
        }

        AudioListener.volume = 0.5f;

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
                foreach (GameObject pipe in pipePrefab)
                {
                    pipe.GetComponentInChildren<Renderer>().material = correctMaterial;
                    winPipe.GetComponent<Renderer>().material = correctMaterial;
                }
            }
        }
    }
}
