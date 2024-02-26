using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshModify : MonoBehaviour
{
    public GameObject meshObject;
    public float modificationAmount = 0.2f;

    private Mesh mesh;
    private Vector3[] originalVertices;
    private int[] verticesCollided;
    private int k=0;

    void Start()
    {
        // Get the Mesh component of the meshObject
        mesh = meshObject.GetComponent<MeshFilter>().mesh;

        // Make a copy of the original vertices
        originalVertices = mesh.vertices;
        verticesCollided = new int[50];
    }

    //void Update()
    //{
    //    // Check for collision with the sphere collider
    //    Vector3 sphereCenter = transform.position;
    //    float sphereRadius = GetComponent<SphereCollider>().radius;
    //    Debug.Log(sphereRadius);
    //    for (int i = 0; i < originalVertices.Length; i++)
    //    {
    //        // Transform the vertex position to world space
    //        Vector3 vertexWorldPos = meshObject.transform.TransformPoint(originalVertices[i]);
    //        float distance = Vector3.Distance(vertexWorldPos, sphereCenter);

    //        // Check if the vertex collides with the sphere
    //        if (distance < sphereRadius)
    //        {
    //            verticesCollided[k] = i;
    //            k++;
    //            //Debug.Log(verticesCollided[k]);
    //        }
    //    }

    //    //// Modify the vertices that collided with the sphere
    //    //Vector3[] modifiedVertices = new Vector3[originalVertices.Length];
    //    //for (int i = 0; i < originalVertices.Length; i++)
    //    //{
    //    //    modifiedVertices[i] = originalVertices[i];
    //    //    if (verticesCollided[i])
    //    //    {
    //    //        Debug.Log(originalVertices[i]);
    //    //        modifiedVertices[i] = transform.position;
    //    //        Debug.Log(transform.position);//Vector3.one * modificationAmount;
    //    //    }
    //    //}
    //}

    public void changeshape()
    {
        // Check for collision with the sphere collider
        Vector3 sphereCenter = transform.position;
        float sphereRadius = GetComponent<SphereCollider>().radius;
        Debug.Log(sphereRadius);
        for (int i = 0; i < originalVertices.Length; i++)
        {
            // Transform the vertex position to world space
            Vector3 vertexWorldPos = meshObject.transform.TransformPoint(originalVertices[i]);
            float distance = Vector3.Distance(vertexWorldPos, sphereCenter);

            // Check if the vertex collides with the sphere
            if (distance < sphereRadius)
            {
                verticesCollided[k] = i;
                k++;
                //Debug.Log(verticesCollided[k]);
            }
        }

        for (int i = 0; i < verticesCollided.Length; i++)
        {
            Debug.Log(verticesCollided[i]);
            originalVertices[verticesCollided[i]].x -= modificationAmount;
        }

        // Apply the modified vertices to the mesh
        mesh.vertices = originalVertices;
        mesh.RecalculateNormals();
    }
}