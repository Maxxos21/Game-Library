using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectInteraction : MonoBehaviour
{
    // Reference
    Renderer rend;
    MainLaser mainLasers;

    // Variables
    public bool isActivated;

    // Material
    [SerializeField] private Material activationMaterial, defaultMaterial;

    void Awake()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (isActivated)
        {
            var mats = rend.materials;
            mats[2] = activationMaterial;
            rend.materials = mats;
        }
    }
}
