using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviewObject : MonoBehaviour
{

    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    public float mSpeed = 1.0f;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            RotateObject();
        }
        mPrevPos = Input.mousePosition;
    }

    void RotateObject()
    {
        mPosDelta = Input.mousePosition - mPrevPos;

        if (Vector3.Dot(transform.up, Vector3.up) >= 0)
        {
            transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right) * mSpeed, Space.World);
        }
        else
        {
            transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right) * mSpeed, Space.World);
        }

        transform.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta, Camera.main.transform.up) * mSpeed, Space.World);
    }

}
