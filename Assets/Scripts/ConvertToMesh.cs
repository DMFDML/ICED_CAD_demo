using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class ConvertToMesh : MonoBehaviour
{
    public Material meshMaterial; // Assign a material for the mesh
    public float meshWidth = 0.1f; // Adjust the width of the mesh

    private MeshFilter meshFilter;
    private MeshRenderer meshRenderer;

    void Start()
    {
        // Ensure there is a LineRenderer component attached
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer component not found on the GameObject.");
            return;
        }

        // Create MeshFilter and MeshRenderer components
        meshFilter = gameObject.AddComponent<MeshFilter>();
        meshRenderer = gameObject.AddComponent<MeshRenderer>();

        // Assign the material to the meshRenderer
        meshRenderer.material = meshMaterial;

        // Generate the mesh
        GenerateMesh(lineRenderer);
    }

    void GenerateMesh(LineRenderer lineRenderer)
    {
        // Get the positions from the LineRenderer
        Vector3[] linePositions = new Vector3[lineRenderer.positionCount];
        Debug.Log(lineRenderer.GetPositions(linePositions));
        lineRenderer.GetPositions(linePositions);

        // Calculate the number of vertices and triangles needed
        int numVertices = linePositions.Length * 2;
        int numTriangles = (linePositions.Length - 1) * 6;

        // Create arrays for vertices and triangles
        Vector3[] vertices = new Vector3[numVertices];
        int[] triangles = new int[numTriangles];

        // Calculate vertices and triangles
        for (int i = 0; i < linePositions.Length; i++)
        {
            // Calculate vertex index for current position
            int vertexIndex = i * 2;

            // Set vertices for current position
            vertices[vertexIndex] = linePositions[i] + new Vector3(0, 0, meshWidth / 2);
            vertices[vertexIndex + 1] = linePositions[i] - new Vector3(0, 0, meshWidth / 2);

            // Set triangles for current position (except for the last position)
            if (i < linePositions.Length - 1)
            {
                int triangleIndex = i * 6;

                triangles[triangleIndex] = vertexIndex;
                triangles[triangleIndex + 1] = vertexIndex + 1;
                triangles[triangleIndex + 2] = vertexIndex + 2;

                triangles[triangleIndex + 3] = vertexIndex + 1;
                triangles[triangleIndex + 4] = vertexIndex + 3;
                triangles[triangleIndex + 5] = vertexIndex + 2;
            }
        }

        // Create and assign the mesh
        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;
    }
}
