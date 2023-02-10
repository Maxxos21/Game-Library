using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject[] mObjects;

    public void SwitchObject(int index)
    {
        // enalbe the object at the given index
        mObjects[index].SetActive(true);

        // disable all other objects
        for (int i = 0; i < mObjects.Length; i++)
        {
            if (i != index)
            {
                mObjects[i].SetActive(false);
            }
        }
    }
}
