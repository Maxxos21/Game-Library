using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorCubeSwitch : MonoBehaviour
{
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material selectedMaterial;
        
    [Header("Cursor")]
    [SerializeField] private Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    void OnMouseDown()
    {
        if (GetComponent<Renderer>().sharedMaterial == defaultMaterial)
        {
            GetComponent<Renderer>().sharedMaterial = selectedMaterial;
        }
        else
        {
            GetComponent<Renderer>().sharedMaterial = defaultMaterial;
        }
    }

    void OnMouseOver()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
}