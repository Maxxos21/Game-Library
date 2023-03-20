using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideObject : MonoBehaviour
{
    // can slide between two points
    // from it's local position

    public enum SlideDirection
    {
        X,
        Y,
        Z
    }

    [Header("Slide Direction")]
    public SlideDirection slideDirection;

    [Header("Slide Points")]
    public Vector3 slidePointA;
    public Vector3 slidePointB;

    [Header("Slide Speed")]
    public float slideSpeed = 1.0f;
    private bool isSliding = false;

    private void Update()
    {
        if (isSliding)
        {
            Slide();
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSliding = true;
        }
    }

    private void OnMouseExit()
    {
        isSliding = false;
    }

    private void Slide()
    {
        switch (slideDirection)
        {
            case SlideDirection.X:
                transform.localPosition = Vector3.Lerp(transform.localPosition, slidePointB, slideSpeed * Time.deltaTime);
                break;
            case SlideDirection.Y:
                transform.localPosition = Vector3.Lerp(transform.localPosition, slidePointB, slideSpeed * Time.deltaTime);
                break;
            case SlideDirection.Z:
                transform.localPosition = Vector3.Lerp(transform.localPosition, slidePointB, slideSpeed * Time.deltaTime);
                break;
            default:
                break;
        }
    }

}
