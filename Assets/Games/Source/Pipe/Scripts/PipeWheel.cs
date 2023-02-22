using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PipeWheel : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.5f;
    [SerializeField] private int letter = 0;
    [SerializeField] private TMP_Text[] letterText; 
    [SerializeField] private Color solvedColor;
    [SerializeField] private GameObject menuCanvas;

    PipeManager pipeManager;

    private void Awake()
    {
        pipeManager = FindObjectOfType<PipeManager>();
    }

    private void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);

        if (pipeManager.isSolved)
        {
            letterText[letter].color = solvedColor;
        }
    }

    public void DisableMenu()
    {
        menuCanvas.SetActive(false);
    }
}
