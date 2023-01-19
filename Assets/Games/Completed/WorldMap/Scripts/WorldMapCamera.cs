using UnityEngine;

public class WorldMapCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;
    private Vector3 dragOrigin;

    [SerializeField] private float zoomStep, zoomMin, zoomMax;

    [SerializeField] private float mapMinX, mapMaxX, mapMinY, mapMaxY;

    // Update is called once per frame
    void Update()
    {
        PanCamera();
        PanCameraArrows();
        ZoomCamera();
    }

    private void PanCamera()
    {
        if (Input.GetMouseButtonDown(1))
        {
            dragOrigin = cam.ScreenToWorldPoint(Input.mousePosition);
        }

        if (Input.GetMouseButton(1))
        {
            Vector3 difference = dragOrigin - cam.ScreenToWorldPoint(Input.mousePosition);

            cam.transform.position = ClampCameraPosition(cam.transform.position + difference);
        }
    }

    private void PanCameraArrows()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            cam.transform.position = ClampCameraPosition(cam.transform.position + new Vector3(0, 0.1f, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            cam.transform.position = ClampCameraPosition(cam.transform.position + new Vector3(0, -0.1f, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            cam.transform.position = ClampCameraPosition(cam.transform.position + new Vector3(-0.1f, 0, 0));
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            cam.transform.position = ClampCameraPosition(cam.transform.position + new Vector3(0.1f, 0, 0));
        }
    }

    private void ZoomCamera()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");

        if (scroll != 0)
        {
            float zoom = cam.orthographicSize;
            zoom -= scroll * zoomStep;
            zoom = Mathf.Clamp(zoom, zoomMin, zoomMax);
            cam.orthographicSize = zoom;

            cam.transform.position = ClampCameraPosition(cam.transform.position);
        }
    }

    private Vector3 ClampCameraPosition(Vector3 position)
    {
        float camHeight = cam.orthographicSize;
        float camWidth = cam.orthographicSize * cam.aspect;

        float minX = mapMinX + camWidth;
        float maxX = mapMaxX - camWidth;
        float minY = mapMinY + camHeight;
        float maxY = mapMaxY - camHeight;

        float clampedX = Mathf.Clamp(position.x, minX, maxX);
        float clampedY = Mathf.Clamp(position.y, minY, maxY);

        return new Vector3(clampedX, clampedY, -10);
    }

}
