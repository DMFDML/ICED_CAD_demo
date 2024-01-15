// Script for creating xray plane for voxel based models
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Xray : MonoBehaviour
{
    //Basic Carve Code
    public GameObject tool;
    public GameObject swarf;
    private Collider[] hitColliders;
    float radius;
    public LayerMask carveLayers;

    // Added for saving voxels: Array to store child locations
    private Vector3[] childLocations;

    // File path to save the CSV file
    private string filePath;

    void Start()
    {
        radius = (tool.GetComponent<Transform>().localScale.x / 2) + 0.05f;
    }

    // Make voxel inactive on colliding with Xray plane
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "voxel")
        {
            //Destroy(col.gameObject);
            col.gameObject.SetActive(false);
        }
    }

    // Make all the voxels active again after xray comes out of box collider of voxel parent
    private void OnCollisionExit(Collision col)
    {
        Debug.Log(col.gameObject.tag);
        if (col.gameObject.tag == "Removable_voxels")
        {
            // Activate all children of the GameObject
            foreach (Transform child in col.gameObject.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    // activate all voxels on calling this function
    public void SetVoxelStateActive()
    {
        // Find the GameObject named "voxelparent"
        GameObject voxelParent = GameObject.Find("Voxel Parent");

        // Check if the GameObject was found
        if (voxelParent != null)
        {
            // Activate all children of the GameObject
            foreach (Transform child in voxelParent.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("GameObject 'voxelparent' not found.");
        }
    }
}