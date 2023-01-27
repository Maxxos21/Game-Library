using UnityEngine;

public class PipeRotate : MonoBehaviour
{
    void Start()
    {
        transform.Rotate(0, Random.Range(0, 4) * 90, 0);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            transform.Rotate(0, 90, 0);
        }
    }
}
