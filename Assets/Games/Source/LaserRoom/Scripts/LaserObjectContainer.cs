using System;
using UnityEngine;

[Serializable]
[ExecuteInEditMode]
public class LaserObjectContainer : MonoBehaviour
{
    [Header("Object Initialization")]
    public bool isMovable = true;
    public bool isRotatable = true;
    public bool isReceiver = false;

    public enum ChildActivationEnum { OneSide, TwoSide, Blocker, Laser, Receiver }
    public ChildActivationEnum activeOption;

    public Material nonmovableMaterial;
    public Material nonrotatableMaterial;
    public Material defaultMaterial;
    public Material hitMat;

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
                SetName("MirrorReceiver");

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

    void OnDrawGizmos()
    {
        Vector3 position = new Vector3(0,2.5f,0);

        if (isReceiver)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(transform.position + position, 0.5f);
        }
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
            LaserObjectDrag mover = child.GetComponent<LaserObjectDrag>();
            LaserObjectRotate rotator = child.GetComponent<LaserObjectRotate>();

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
