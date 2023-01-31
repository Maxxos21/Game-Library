using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitcher : MonoBehaviour
{
    [SerializeField] private GameObject objectOne;
    [SerializeField] private GameObject objectTwo;

    public void Switch()
    {
        objectOne.SetActive(!objectOne.activeSelf);
        objectTwo.SetActive(!objectTwo.activeSelf);
    }
}
