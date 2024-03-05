using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshModify : MonoBehaviour
{
    public GameObject meshObject;
    //public float modificationAmount;

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
        verticesCollided = new int[100];
        //modifiedVertices = originalVertices;
        //modificationAmount = 0.01f;
    }

    void Update()
    {
            if (updateshapeflag == true)
            {
                for (int i = 0; i < verticesCollided.Length; i++)
                {
                //Debug.Log(verticesCollided[i]);
                    //Debug.Log(originalVertices[verticesCollided[1]].x);
                    //Debug.Log(sphereCenter);
                    Vector3 sphereCenterNew = transform.position;
                    //Debug.Log(sphereCenterNew);
                    //float distance = Vector3.Distance(sphereCenterNew, sphereCenter);
                Vector3 distance = sphereCenterNew - sphereCenter;
                //Debug.Log(distance);

                // Convert vertices from local space to world space
                Vector3 distanceInLocalSpace = meshObject.transform.InverseTransformDirection(distance);


                //Debug.Log(distanceInLocalSpace);

                if (Mathf.Abs(distanceInLocalSpace.x) > Mathf.Abs(distanceInLocalSpace.y))
                {
                    if (Mathf.Abs(distanceInLocalSpace.x) > Mathf.Abs(distanceInLocalSpace.z))
                    {
                        if (verticesCollided[i] > 0)
                        {
                            modifiedVertices[verticesCollided[i]].x = originalVertices[verticesCollided[i]].x + distanceInLocalSpace.x;
                            modifiedVertices[verticesCollided[i]].y = originalVertices[verticesCollided[i]].y;
                            modifiedVertices[verticesCollided[i]].z = originalVertices[verticesCollided[i]].z;
                        }
                    }
                    else
                    {
                        if (verticesCollided[i] > 0)
                        {
                            modifiedVertices[verticesCollided[i]].z = originalVertices[verticesCollided[i]].z + distanceInLocalSpace.z;
                            modifiedVertices[verticesCollided[i]].x = originalVertices[verticesCollided[i]].x;
                            modifiedVertices[verticesCollided[i]].y = originalVertices[verticesCollided[i]].y;
                        }
                    }
                }
                else if (Mathf.Abs(distanceInLocalSpace.y) > Mathf.Abs(distanceInLocalSpace.z))
                {
                    if (verticesCollided[i] > 0)
                    {
                        modifiedVertices[verticesCollided[i]].y = originalVertices[verticesCollided[i]].y + distanceInLocalSpace.y;
                        modifiedVertices[verticesCollided[i]].x = originalVertices[verticesCollided[i]].x;
                        modifiedVertices[verticesCollided[i]].z = originalVertices[verticesCollided[i]].z;
                    }
                }
                }
                // Apply the modified vertices to the mesh
                //Debug.Log("still updating");
                mesh.vertices = modifiedVertices;
                mesh.RecalculateNormals();
            }
    }

    public void changeshape()
    {
        // Check for collision with the sphere collider
        //Debug.Log(k);
        originalVertices = mesh.vertices;
        sphereCenter = transform.position;
        //Debug.Log(sphereCenter);
        
        float sphereRadius = GetComponent<SphereCollider>().radius;
        Color[] colors = new Color[originalVertices.Length];
        for (int i = 0; i < originalVertices.Length; i++)
        {
            // Transform the vertex position to world space
            Vector3 vertexWorldPos = meshObject.transform.TransformPoint(originalVertices[i]);
            float distance = Vector3.Distance(vertexWorldPos, sphereCenter);
            // Check if the vertex collides with the sphere
            if ((distance < sphereRadius)&&(k<100))
            {
                verticesCollided[k] = i;
                k++;
                Debug.Log(originalVertices[verticesCollided[i]]);
                // Color the collided vertices (you can modify this part according to your visualization needs)
                colors[i] = Color.red; // Set the color of collided vertices to red
            }
        }

        modifiedVertices = (Vector3[])originalVertices.Clone();
        mesh.colors = colors; // Apply colors to the mesh
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
        k=0;
        for (int i = 0; i < verticesCollided.Length; i++)
        {
            verticesCollided[i]=0;
        }
    }
}