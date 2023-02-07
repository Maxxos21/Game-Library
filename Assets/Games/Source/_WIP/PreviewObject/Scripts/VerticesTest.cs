using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticesTest : MonoBehaviour
{
    public Mesh mesh;
    Vector3[] vertices;
    Vector3[] normals;
    int[] triangles;
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

    void Awake()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        normals = mesh.normals;
        triangles = mesh.triangles;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SliceMesh();
            Debug.Log("SliceMesh");
        }
    }

    void CreateNewVertices()
    {
        int i = Random.Range(0, triangles.Length - 3);
        Vector3[] newVertices = new Vector3[vertices.Length];

        newVertices[triangles[i]] = vertices[triangles[i]];
        newVertices[triangles[i + 1]] = vertices[triangles[i + 1]];
        newVertices[triangles[i + 2]] = vertices[triangles[i + 2]];

        mesh.vertices = newVertices;
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }

    void SliceMesh()
    {
        Vector3 point = Input.mousePosition;
        float minDistance = float.MaxValue;
        int nearestVertexIndex = -1;

        for (int i = 0; i < vertices.Length; i++)
        {
            float distance = Vector3.Distance(Camera.main.WorldToScreenPoint(vertices[i]), point);

            if (distance < minDistance)
            {
                minDistance = distance;
                nearestVertexIndex = i;
            }
        }

        if (nearestVertexIndex == -1)
        {
            return;
        }

        int i0 = nearestVertexIndex;
        int i1 = (nearestVertexIndex + 1) % vertices.Length;
        int i2 = (nearestVertexIndex + 2) % vertices.Length;

        List<Vector3> newVertices = new List<Vector3>(vertices);
        List<int> newTriangles = new List<int>(triangles);

        newVertices.RemoveAt(i0);
        newVertices.RemoveAt(i1);
        newVertices.RemoveAt(i2);

        for (int i = 0; i < newTriangles.Count; i++)
        {
            if (newTriangles[i] == i0)
            {
                newTriangles.RemoveAt(i);
                i--;
            }
            else if (newTriangles[i] > i0)
            {
                newTriangles[i]--;
            }

            if (newTriangles[i] == i1)
            {
                newTriangles.RemoveAt(i);
                i--;
            }
            else if (newTriangles[i] > i1)
            {
                newTriangles[i]--;
            }

            if (newTriangles[i] == i2)
            {
                newTriangles.RemoveAt(i);
                i--;
            }
            else if (newTriangles[i] > i2)
            {
                newTriangles[i]--;
            }
        }

        mesh.SetVertices(newVertices);
        mesh.SetTriangles(newTriangles, 0);
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
    }
}