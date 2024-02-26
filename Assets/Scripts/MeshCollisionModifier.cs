using UnityEngine;

public class MeshCollisionModifier : MonoBehaviour
{
    public GameObject meshObject;
    public float modificationAmount = 5f;

    private Mesh mesh;
    private Vector3[] originalVertices;
    private bool[] verticesCollided;

    void Start()
    {
        // Get the Mesh component of the meshObject
        mesh = meshObject.GetComponent<MeshFilter>().mesh;

        // Make a copy of the original vertices
        originalVertices = mesh.vertices;
        verticesCollided = new bool[originalVertices.Length];
    }

    void Update()
    {
        // Check for collision with the sphere collider
        Vector3 sphereCenter = transform.position;
        float sphereRadius = GetComponent<SphereCollider>().radius;
        for (int i = 0; i < originalVertices.Length; i++)
        {
            // Transform the vertex position to world space
            Vector3 vertexWorldPos = meshObject.transform.TransformPoint(originalVertices[i]);
            float distance = Vector3.Distance(vertexWorldPos, sphereCenter);

            // Check if the vertex collides with the sphere
            if (distance < sphereRadius)
            {
                verticesCollided[i] = true;
            }
            else
            {
                verticesCollided[i] = false;
            }
        }

        // Modify the vertices that collided with the sphere
        Vector3[] modifiedVertices = new Vector3[originalVertices.Length];
        for (int i = 0; i < originalVertices.Length; i++)
        {
            modifiedVertices[i] = originalVertices[i];
            if (verticesCollided[i])
            {
                modifiedVertices[i] += Vector3.one * modificationAmount;
            }
        }

        // Apply the modified vertices to the mesh
        mesh.vertices = modifiedVertices;
        mesh.RecalculateNormals();
    }
}