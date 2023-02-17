using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ReceiverLogic : MonoBehaviour
{
    // Reference
    Renderer rend;
    Laser lasers;

    // Variables
    public bool isActivated;

    // Material
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material activationMaterial;
    private Material[] materials;
    private UnityEvent onActivated;
    private UnityEvent onDeactivated;

    private void Awake()
    {
        materials = new Material[] {defaultMaterial, activationMaterial};
        rend = GetComponent<Renderer>();
    }

    void Start()
    {
        // create new UnityEvents if they are not already created
        if (onActivated == null)
        {
            onActivated = new UnityEvent();
        }
        if (onDeactivated == null)
        {
            onDeactivated = new UnityEvent();
        }

        // subscribe to the onActivated event
        onActivated.AddListener(ActivateObject);
        onDeactivated.AddListener(DeactivateObject);
    }

    public void ActivateObject()
    {
        var mats = rend.materials;
        mats[1] = materials[1];
        rend.materials = mats;
        Debug.Log("Activated");
    }

    public void DeactivateObject()
    {
        var mats = rend.materials;
        mats[1] = materials[0];
        rend.materials = mats;
        Debug.Log("Deactivated");
    }
}


