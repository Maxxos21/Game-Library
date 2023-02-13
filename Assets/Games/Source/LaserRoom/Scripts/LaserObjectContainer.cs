using System;
using UnityEngine;

[Serializable]
[ExecuteInEditMode]
public class LaserObjectContainer : MonoBehaviour
{
    [Header("Object Initialization")]
    public bool isMovable = true;
    public bool isRotatable = true;
    public enum ChildActivationEnum { OneSide, TwoSide, Blocker, Laser, Receiver }
    public ChildActivationEnum activeOption;

    public Material movableMaterial;
    public Material rotatableMaterial;
    public Material bothMovableAndRotatableMaterial;

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
            case ChildActivationEnum.OneSide:
                transform.GetChild(0).gameObject.SetActive(true);
                SetName("One Side");
                break;
            case ChildActivationEnum.TwoSide:
                transform.GetChild(1).gameObject.SetActive(true);
                SetName("Two Side");
                break;
            case ChildActivationEnum.Blocker:
                transform.GetChild(2).gameObject.SetActive(true);
                SetName("Blocker");
                break;
            case ChildActivationEnum.Laser:
                transform.GetChild(3).gameObject.SetActive(true);
                SetName("Laser");
                break;
            case ChildActivationEnum.Receiver:
                transform.GetChild(4).gameObject.SetActive(true);
                SetName("Receiver");
                break;
        }

        if (activeOption == ChildActivationEnum.Laser || activeOption == ChildActivationEnum.Receiver) return;
        
        SetMaterialBasedOnProperties();
    }

    void SetMaterialBasedOnProperties()
    {
        Material materialToUse = null;
        if (isMovable && isRotatable)
        {
            materialToUse = bothMovableAndRotatableMaterial;
        }
        else if (isMovable)
        {
            materialToUse = movableMaterial;
        }
        else if (isRotatable)
        {
            materialToUse = rotatableMaterial;
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

    void SetName(string name)
    {
        gameObject.name = name;
    }
}
