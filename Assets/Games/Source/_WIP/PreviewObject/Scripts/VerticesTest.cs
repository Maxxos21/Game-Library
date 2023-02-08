using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticesTest : MonoBehaviour
{
    public Mesh mesh;
    public float gizmoSize = 0.1f;
    public Color gizmoColor = Color.red;

    private void OnDrawGizmos()
    {
        if (mesh == null) return;

        Vector3[] vertices = mesh.vertices;
        Gizmos.color = gizmoColor;

        foreach (Vector3 vertex in vertices)
        {
            Gizmos.DrawSphere(transform.TransformPoint(vertex), gizmoSize);
        }
    }
}

