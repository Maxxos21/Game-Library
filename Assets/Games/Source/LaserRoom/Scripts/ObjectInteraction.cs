using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ObjectInteraction : MonoBehaviour
{
    // Reference
    Renderer rend;
    MainLaser mainLasers;

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

    // Use this for initialization
    void Start()
    {
        // Initialize the Unity Events
        if (onActivated == null)
        {
            onActivated = new UnityEvent();
        }
        if (onDeactivated == null)
        {
            onDeactivated = new UnityEvent();
        }
    }

    public bool IsActivated
    {
        get { return isActivated; }
        set
        {
            isActivated = value;

            if (isActivated)
            {
                var mats = rend.materials;
                mats[2] = materials[1];
                rend.materials = mats;

                Debug.Log("Activated");
                onActivated.Invoke();
            }
            else
            {
                var mats = rend.materials;
                mats[2] = materials[0];
                rend.materials = mats;
                
                Debug.Log("Deactivated");
                onDeactivated.Invoke();
            }
        }
    }
}
