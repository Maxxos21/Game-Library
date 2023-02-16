using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PipeWheel : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.5f;
    [SerializeField] private int letter = 0;
    [SerializeField] private TMP_Text[] letterText; 

    private void Update()
    {
        transform.Rotate(0, rotationSpeed, 0);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            letterText[letter].color = Color.red;
        }
    }
}
