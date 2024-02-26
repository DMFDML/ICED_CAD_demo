using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshModify : MonoBehaviour
{
    public GameObject meshObject;
    public float modificationAmount;

    private Mesh mesh;
    private Vector3[] originalVertices;
    private Vector3[] modifiedVertices;
    private int[] verticesCollided;
    private int k=0;
    private bool updateshapeflag=false;
    private Vector3 sphereCenter;
    
    void Start()
    {
        // Get the Mesh component of the meshObject
        mesh = meshObject.GetComponent<MeshFilter>().mesh;

        // Make a copy of the original vertices
        //originalVertices = mesh.vertices;
        verticesCollided = new int[50];
        //modifiedVertices = originalVertices;
        modificationAmount = 0.01f;
    }

    void Update()
    {
            if (updateshapeflag == true)
            {
                for (int i = 0; i < verticesCollided.Length; i++)
                {
                //Debug.Log(verticesCollided[i]);
                    Debug.Log(originalVertices[verticesCollided[1]].x);
                    //Debug.Log(sphereCenter);
                    Vector3 sphereCenterNew = transform.position;
                    //Debug.Log(sphereCenterNew);
                    float distance = Vector3.Distance(sphereCenterNew, sphereCenter);
                    Debug.Log(distance);
                    if (verticesCollided[i] > 0)
                    {
                        modifiedVertices[verticesCollided[i]].x = originalVertices[verticesCollided[i]].x - distance;
                    }
                    Debug.Log(modifiedVertices[verticesCollided[1]].x);

                }
                // Apply the modified vertices to the mesh
                //Debug.Log("still updating");
                mesh.vertices = modifiedVertices;
                mesh.RecalculateNormals();
            }

        
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
    }

    public void changeshape()
    {
        // Check for collision with the sphere collider
        originalVertices = mesh.vertices;
        sphereCenter = transform.position;
        Debug.Log(sphereCenter);
        float sphereRadius = GetComponent<SphereCollider>().radius;
        //Debug.Log(sphereRadius);
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

        modifiedVertices = (Vector3[])originalVertices.Clone(); ;
        updateshapeflag =true;

        //for (int i = 0; i < verticesCollided.Length; i++)
        //{
        //    Debug.Log(verticesCollided[i]);
        //    originalVertices[verticesCollided[i]].x -= modificationAmount;
        //}

        //// Apply the modified vertices to the mesh
        //mesh.vertices = originalVertices;
        //mesh.RecalculateNormals();
    }

    public void stopchanging()
    {
        updateshapeflag=false;
        Debug.Log("Changed Flag");
    }
}