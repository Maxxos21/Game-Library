using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPlaceableObject : MonoBehaviour
{
    // onclick rotate 90 degrees
    private void OnMouseDown()
    {
        transform.Rotate(0, 90, 0);
    }
}
