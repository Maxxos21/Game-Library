using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perspective : MonoBehaviour
{

    enum PerspectiveEnum { Top, Front };
    [SerializeField] private PerspectiveEnum perspective = PerspectiveEnum.Top;

    // set the vector3 for rotation and position
    [SerializeField] private Vector3 topRotation = new Vector3(90, 0, 0);
    [SerializeField] private Vector3 topPosition = new Vector3(0, 10, 0);

    [SerializeField] private Vector3 frontRotation = new Vector3(0, 0, 0);
    [SerializeField] private Vector3 frontPosition = new Vector3(0, 0, -10);

    void Start()
    {
        ChangePerspective();
    }

    public void ChangePerspective()
    {
        if (perspective == PerspectiveEnum.Top)
        {
            Camera.main.transform.rotation = Quaternion.Euler(topRotation);
            Camera.main.transform.position = topPosition;
            perspective = PerspectiveEnum.Front;
        }
        else
        {
            Camera.main.transform.rotation = Quaternion.Euler(frontRotation);
            Camera.main.transform.position = frontPosition;
            perspective = PerspectiveEnum.Top;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangePerspective();
        }
    }
}
