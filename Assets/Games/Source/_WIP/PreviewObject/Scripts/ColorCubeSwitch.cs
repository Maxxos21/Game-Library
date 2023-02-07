using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCubeSwitch : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material selectedMaterial;

    void OnMouseDown()
    {
        if (GetComponent<Renderer>().sharedMaterial == defaultMaterial)
        {
            GetComponent<Renderer>().sharedMaterial = selectedMaterial;
        }
        else
        {
            GetComponent<Renderer>().sharedMaterial = defaultMaterial;
        }
    }
}