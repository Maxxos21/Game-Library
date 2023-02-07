using UnityEngine;

public class PreviewObject : MonoBehaviour
{
    Vector3 mPrevPos, mPosDelta = Vector3.zero;
    public float mSpeed = 1.0f;

    void Update()
    {
        RotateObject();
    }

    void RotateObject()
    {
        if (Input.GetMouseButton(1))
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
        mPrevPos = Input.mousePosition;
    }
}
