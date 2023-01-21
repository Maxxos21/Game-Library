using System.Collections;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 0.5f;

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            RotateY(rotationSpeed);
        }
        
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            RotateY(-rotationSpeed);
        }
    }

    public void RotateY(float angle)
    {
        transform.Rotate(0, angle, 0);
    }
}