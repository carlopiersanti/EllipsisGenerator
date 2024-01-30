using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class EllipsisGenerator : MonoBehaviour
{
    public int resolution = 32;
    public float radiusX = 2f;
    public float radiusY = 3f;
    public float radiusZ = 1f;

    void Start()
    {
        GenerateEllipsoid();
    }

    void GenerateEllipsoid()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        Mesh mesh = new Mesh();

        meshFilter.mesh = mesh;

        Vector3[] vertices = new Vector3[resolution * resolution];

        for (int i = 0; i < resolution; i++)
        {
            float phi = 2 * Mathf.PI * i / (resolution - 1);
            for (int j = 0; j < resolution; j++)
            {
                float theta = Mathf.PI * j / (resolution - 1);
                float x = Mathf.Sin(theta) * Mathf.Cos(phi) * radiusX;
                float y = Mathf.Cos(theta) * radiusY;
                float z = Mathf.Sin(theta) * Mathf.Sin(phi) * radiusZ;

                vertices[i * resolution + j] = new Vector3(x, y, z);
            }
        }

        mesh.vertices = vertices;

        int[] triangles = new int[6 * (resolution - 1) * (resolution - 1)];

        int index = 0;

        for (int i = 0; i < resolution - 1; i++)
        {
            for (int j = 0; j < resolution - 1; j++)
            {
                int a = i * resolution + j;
                int b = i * resolution + j + 1;
                int c = (i + 1) * resolution + j;
                int d = (i + 1) * resolution + j + 1;

                triangles[index++] = a;
                triangles[index++] = b;
                triangles[index++] = c;

                triangles[index++] = b;
                triangles[index++] = d;
                triangles[index++] = c;
            }
        }

        mesh.triangles = triangles;

        mesh.RecalculateNormals();
    }
}
