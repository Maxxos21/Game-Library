using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject cube, sphere;

    public void SwitchObject()
    {
        cube.SetActive(!cube.activeSelf);
        sphere.SetActive(!sphere.activeSelf);
    }
}
