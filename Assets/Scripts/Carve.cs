using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carve : MonoBehaviour
{

    public GameObject tool;
    public GameObject swarf;
    private Collider[] hitColliders;

    // Array to store child locations
    private Vector3[] childLocations;

    float radius;
        
    public LayerMask carveLayers;

    void Start() 
    {
        radius = (tool.GetComponent<Transform>().localScale.x / 2) + 0.05f;
    }

    private void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.tag == "voxel")
        {
            Destroy(col.gameObject);
        }
    }

    public void SetShapeName()
    {
        GameObject voxelParent = GameObject.Find("Voxel Parent");

        // Check if the GameObject is found
        if (voxelParent != null)
        {
            // Print the position of the voxelParent
            Debug.Log("Voxel Parent Location: " + voxelParent.transform.position);
        }
        else
        {
            // Print an error message if the GameObject is not found
            Debug.LogError("GameObject named 'voxelParent' not found in the scene.");
        }

        // Get the number of children
        int childCount = voxelParent.transform.childCount;

        // Initialize the array to store child locations
        childLocations = new Vector3[childCount];

        // Loop through each child and save its position
        for (int i = 0; i < childCount; i++)
        {
            Transform child = voxelParent.transform.GetChild(i);
            childLocations[i] = child.localPosition;
        }

        // Print the first 10 values of childLocations
        int printCount = Mathf.Min(10, childLocations.Length);

        for (int i = 0; i < printCount; i++)
        {
            Debug.Log($"Child Location {i + 1}: {childLocations[i]}");
        }
        // Now, the childLocations array contains the positions of all children
        // You can use this array for further processing or storage
    }
}
