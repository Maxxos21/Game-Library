using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using System;

public class InspectItem : MonoBehaviour
{
    [SerializeField] private GameObject inspectPanel;
    public static bool isInspecting = false;
    Outline _outline;


    private void Awake()
    {
        _outline = GetComponent<Outline>();
    }

    void OnMouseOver()
    {
        _outline.OutlineMode = Outline.Mode.OutlineAll;

        if (Input.GetMouseButtonDown(0) && !isInspecting)
        {
            isInspecting = true;
            inspectPanel.SetActive(true);
        }
    }

    void OnMouseExit()
    {
        _outline.OutlineMode = Outline.Mode.None;
    }

}
