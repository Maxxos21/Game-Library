using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotate : MonoBehaviour
{
    public GameObject objectToRotate;
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    public float mSpeed = 1.0f;
    bool mIsRotating = false;
    public bool mClockwise = true;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (IsMouseOverCircle())
            {
                mIsRotating = true;
                mPrevPos = Input.mousePosition;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            mIsRotating = false;
        }

        if (mIsRotating)
        {
            mPosDelta = Input.mousePosition - mPrevPos;
            RotateCircle();
            mPrevPos = Input.mousePosition;
        }
    }

    bool IsMouseOverCircle()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            return hit.collider.gameObject == gameObject;
        }
        return false;
    }

    void RotateCircle()
    {
        float direction = mClockwise ? -1.0f : 1.0f;
        objectToRotate.transform.Rotate(Vector3.forward, direction * Vector3.Dot(mPosDelta, Camera.main.transform.up) * mSpeed, Space.World);
    }
}