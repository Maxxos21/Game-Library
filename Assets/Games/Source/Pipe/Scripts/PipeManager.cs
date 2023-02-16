using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition;
using UnityEngine.SceneManagement;

public class PipeManager : MonoBehaviour
{
    [SerializeField] private GameObject gamePipe;
    [SerializeField] public List<GameObject> pipePrefab = new List<GameObject>();
    private List<int> solution = new List<int>();
    private List<int> currentArrangement = new List<int>();
    public bool isSolved = false;
    [SerializeField] private Material defaultMaterial, correctMaterial;
    [SerializeField] private GameObject winPipe;
    [SerializeField] private GameObject winVFX;
    [SerializeField] private GameObject continueButton;

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
            else if (pipeLevelCreator.activeOption == PipeLevelCreator.ChildActivationEnum.Cross)
            {
                pipeLevelCreator.rotation = 0;
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
                    isSolved = true;
                    AudioPlayer.Instance.PlayAudio(0);
                    StartCoroutine(ChangePipeColor(defaultMaterial, correctMaterial, 0.10f));
                }
            }
        }
    }

    private IEnumerator ChangePipeColor(Material startMaterial, Material endMaterial, float duration)
    {
        float elapsedTime = 0f;

        for (int i = 0; i < pipePrefab.Count; i++)
        {
            elapsedTime = 0f;
            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                Renderer renderer = pipePrefab[i].GetComponentInChildren<Renderer>();
                renderer.material.color = Color.Lerp(startMaterial.color, endMaterial.color, elapsedTime / duration);

                yield return null;
            }
        }

        winPipe.GetComponentInChildren<Renderer>().material = correctMaterial;
        winVFX.SetActive(true);

        if (continueButton != null)
        {
            continueButton.SetActive(true);
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
