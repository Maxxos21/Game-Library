using UnityEngine;
using System.Collections;

public class PreviewObject : MonoBehaviour
{

    public enum RotationAxes
	{
		MouseXAndY,
		MouseX,
		MouseY
	}

    [Header("Object Rotation")]
	public RotationAxes axes;
    private bool isRotating = false;
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    [SerializeField] private float sensitivity = 1.0f;
    [SerializeField] private Vector3 clampMin;
    [SerializeField] private Vector3 clampMax;

    [Header("Cursor")]
    [SerializeField] private Texture2D cursorTexture;
    private CursorMode cursorMode = CursorMode.Auto;
    private Vector2 hotSpot = Vector2.zero;

    void Update()
    {
        if (isRotating)
        {
            mPosDelta = Input.mousePosition - mPrevPos;
            mPrevPos = Input.mousePosition;

            Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
            RotateObject();
        }
        else
        {
            mPrevPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
            isRotating = false;
        }
    }

    void OnMouseOver()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
        }
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode);
    }
    
    void RotateObject()
    {

        if (axes == RotationAxes.MouseXAndY)
        {
            if (Vector3.Dot(transform.up, Vector3.up) >= 0)
            {
                transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right) * sensitivity, Space.World);
            }
            else
            {
                transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right) * sensitivity, Space.World);
            }

            transform.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta, Camera.main.transform.up) * sensitivity, Space.World);
        }
        else if (axes == RotationAxes.MouseX)
        {
            if (Vector3.Dot(transform.up, Vector3.up) >= 0)
            {
                transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right) * sensitivity, Space.World);
            }
            else
            {
                transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right) * sensitivity, Space.World);
            }
        }
        else
        {
            transform.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta, Camera.main.transform.up) * sensitivity, Space.World);
        }
    }
}