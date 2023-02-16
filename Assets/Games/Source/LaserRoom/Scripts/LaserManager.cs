using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class LaserManager : MonoBehaviour
{
    ObjectInteraction[] objectInteractions;

    void Awake()
    {
        objectInteractions = FindObjectsOfType<ObjectInteraction>();
    }

    public void CheckIfAllActivated()
    {
        bool allActivated = true;

        foreach (ObjectInteraction objectInteraction in objectInteractions)
        {
            if (!objectInteraction.IsActivated)
            {
                allActivated = false;
                break;
            }
        }

        if (allActivated)
        {
            Debug.Log("All activated");
        }
    }
}