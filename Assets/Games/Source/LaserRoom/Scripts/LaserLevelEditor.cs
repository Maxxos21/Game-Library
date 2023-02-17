using System;
using UnityEngine;

[Serializable]
[ExecuteInEditMode]
public class LaserLevelEditor : MonoBehaviour
{
    [Header("Object Initialization")]
    public bool isMovable = true;
    public bool isRotatable = true;

    public enum ChildActivationEnum { Single, Double, Gate, SingleReceiver, Seperator, Blocker }
    [Header("Object Type")]
    public ChildActivationEnum activeOption;
    [HideInInspector]
    [SerializeField] public Material nonmovableMaterial, nonrotatableMaterial, defaultMaterial, hitMat;

    private void OnValidate()
    {
        if (!Application.isPlaying)
        {
            Invoke("UpdateChildren", 0.1f);
        }
    }

    void UpdateChildren()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.gameObject.SetActive(false);
        }

        switch (activeOption)
        {
            case ChildActivationEnum.Single:
                transform.GetChild(0).gameObject.SetActive(true);
                SetName("Single Mirror");
                break;
            case ChildActivationEnum.Double:
                transform.GetChild(1).gameObject.SetActive(true);
                SetName("Double Mirror");
                break;
            case ChildActivationEnum.Gate:
                transform.GetChild(2).gameObject.SetActive(true);
                SetName("Gate");
                break;
            case ChildActivationEnum.SingleReceiver:
                transform.GetChild(3).gameObject.SetActive(true);
                SetName("Single + Receiver");
                break;
            case ChildActivationEnum.Seperator:
                transform.GetChild(4).gameObject.SetActive(true);
                SetName("Seperator");
                break;
            case ChildActivationEnum.Blocker:
                transform.GetChild(5).gameObject.SetActive(true);
                SetName("Blocker");
                break;
        }

        SetMaterialBasedOnProperties();
    }

    void SetMaterialBasedOnProperties()
    {
        Material materialToUse = null;

        if (!isMovable)
        {
            materialToUse = nonmovableMaterial;
        }
        else if (!isRotatable)
        {
            materialToUse = nonrotatableMaterial;
        }
        else
        {
            materialToUse = defaultMaterial;
            EnableMovingAndRotating();
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Renderer renderer = child.GetComponent<Renderer>();

            if (renderer != null && materialToUse != null)
            {
                renderer.material = materialToUse;
            }
        }
    }

    void EnableMovingAndRotating()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            Dragger mover = child.GetComponent<Dragger>();
            Rotater rotator = child.GetComponent<Rotater>();

            if (mover != null)
            {
                mover.enabled = true;
            }

            if (rotator != null)
            {
                rotator.enabled = true;
            }
        }
    }

    void SetName(string name)
    {
        int index = transform.GetSiblingIndex();
        gameObject.name = name + " " + index;
    }

}
