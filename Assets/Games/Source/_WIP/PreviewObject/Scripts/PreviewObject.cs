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
    [SerializeField] private float step;

    void Update()
    {
        if (isRotating)
        {
            mPosDelta = Input.mousePosition - mPrevPos;
            mPrevPos = Input.mousePosition;
            RotateObject();
        }
        else
        {
            mPrevPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isRotating = false;
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isRotating = true;
        }
    }

    void RotateObject()
    {
        float rotationAngle;
        if (axes == RotationAxes.MouseXAndY)
        {
            if (Vector3.Dot(transform.up, Vector3.up) >= 0)
            {
                rotationAngle = -Vector3.Dot(mPosDelta, Camera.main.transform.right) * sensitivity;
            }
            else
            {
                rotationAngle = Vector3.Dot(mPosDelta, Camera.main.transform.right) * sensitivity;
            }
            transform.Rotate(transform.up, rotationAngle, Space.World);
            rotationAngle = Vector3.Dot(mPosDelta, Camera.main.transform.up) * sensitivity;
        }
        else if (axes == RotationAxes.MouseX)
        {
            if (Vector3.Dot(transform.up, Vector3.up) >= 0)
            {
                rotationAngle = -Vector3.Dot(mPosDelta, Camera.main.transform.right) * sensitivity;
            }
            else
            {
                rotationAngle = Vector3.Dot(mPosDelta, Camera.main.transform.right) * sensitivity;
            }
        }
        else
        {
            rotationAngle = Vector3.Dot(mPosDelta, Camera.main.transform.up) * sensitivity;
        }

        float roundedAngle = Mathf.Round(rotationAngle / step) * step;
        transform.Rotate(Camera.main.transform.right, roundedAngle, Space.World);
    }
}