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
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    public float sensitivity = 1.0f;
    bool isRotating = false;

    [Header("Cursor")]
    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;

    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverCollider())
            {
                Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
                isRotating = true;
                mPrevPos = Input.mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }

        if (isRotating)
        {
            mPosDelta = Input.mousePosition - mPrevPos;
            RotateObject();
            mPrevPos = Input.mousePosition;
        }
    }

    bool IsMouseOverCollider()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject == gameObject;
        }
        return false;
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