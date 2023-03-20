using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
