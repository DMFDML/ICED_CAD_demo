using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using UnityEditor;

public class Carve : MonoBehaviour
{
    //Basic Carve Code
    public GameObject tool;
    public GameObject swarf;
    private Collider[] hitColliders;
    float radius;        
    public LayerMask carveLayers;

    // Added for saving voxels: Array to store child locations
    private Vector3[] childLocations;
    private int k = 1;
    // File path to save the CSV file
    private string filePath;
    private string scriptPath = "C:\\Users\\ne23946\\IntelligentCAD\\GenerateFeature.py";
    private string freecadCmdPath = @"C:\\Program Files\\FreeCAD 0.20\\bin\\FreeCADCmd.exe";


    void Start() 
    {
        radius = (tool.GetComponent<Transform>().localScale.x / 2) + 0.05f;
    }

    private void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.tag == "voxel")
        {
            Destroy(col.gameObject);
            //col.gameObject.SetActive(false);
            //Transform child = col.gameObject.transform;
            //Debug.Log(child);
            //// Initialize the array to store child locations
            //childLocations[k] = new Vector3(child.localPosition.x, child.localPosition.y, child.localPosition.z);
            //k=k+1;
        }
    }

    //// Code for retrieving removed voxels and save the features one by one as separate csv files for data generation
    //public void SetShapeName()
    //{
    //    // Set the file path
    //    filePath = Application.dataPath + $"/features/feature {k}.csv";
    //    k = k + 1;
    //    Debug.Log("Feature number" + k);

    //    GameObject voxelParent = GameObject.Find("Voxel Parent");

    //    // Check if the GameObject is found
    //    if (voxelParent != null)
    //    {
    //        // Print the position of the voxelParent
    //        Debug.Log("Voxel Parent Location: " + voxelParent.transform.position);
    //    }
    //    else
    //    {
    //        // Print an error message if the GameObject is not found
    //        Debug.LogError("GameObject named 'voxelParent' not found in the scene.");
    //    }

    //    // Get the number of children
    //    int childCount = voxelParent.transform.childCount;

    //    // Initialize the array to store child locations
    //    childLocations = new Vector3[childCount];

    //    // Loop through each child and save its position
    //    for (int i = 0; i < childCount; i++)
    //    {
    //        Transform child = voxelParent.transform.GetChild(i);
    //        childLocations[i] = new Vector3(child.localPosition.x, child.localPosition.y, child.localPosition.z);
    //    }

    //    // Print the first 10 values of childLocations
    //    int printCount = Mathf.Min(10, childLocations.Length);

    //    for (int i = 0; i < printCount; i++)
    //    {
    //        Debug.Log($"Child Location {i + 1}: {childLocations[i]}");
    //    }
    //    // Now, the childLocations array contains the positions of all children
    //    // You can use this array for further processing or storage

    //    // Create or overwrite the file
    //    using (StreamWriter sw = new StreamWriter(filePath))
    //    {
    //        // Write header
    //        //sw.WriteLine("Child Locations");

    //        // Write data
    //        for (int i = 0; i < childLocations.Length; i++)
    //        {
    //            //Debug.Log($"{childLocations[i].x}, {childLocations[i].y}, {childLocations[i].z}");
    //            sw.WriteLine($"{childLocations[i].x}, {childLocations[i].y}, {childLocations[i].z}");
    //        }
    //        Debug.Log("Check");
    //        Debug.Log("Variables saved to CSV file");
    //    }
    //}

    // Code for retrieving removed voxels and recognize features and generate it in CAD model
    public void SetShapeName()
    {
        // Set the file path
        filePath = Application.dataPath + $"/features/feature 1.csv";
        GameObject voxelParent = GameObject.Find("Voxel Parent");

        // Check if the GameObject is found
        if (voxelParent != null)
        {
            // Print the position of the voxelParent
            UnityEngine.Debug.Log("Voxel Parent Location: " + voxelParent.transform.position);
        }
        else
        {
            // Print an error message if the GameObject is not found
            UnityEngine.Debug.LogError("GameObject named 'voxelParent' not found in the scene.");
        }

        // Get the number of children
        int childCount = voxelParent.transform.childCount;

        // Initialize the array to store child locations
        childLocations = new Vector3[childCount];

        // Create or overwrite the file
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            // Write header
            //sw.WriteLine("Child Locations");
            // Loop through each child and save its position
            for (int i = 0; i < childCount; i++)
            {
                Transform child = voxelParent.transform.GetChild(i);
                childLocations[i] = new Vector3(child.localPosition.x, child.localPosition.y, child.localPosition.z);
                sw.WriteLine($"{childLocations[i].x}, {childLocations[i].y}, {childLocations[i].z}");
            }
            UnityEngine.Debug.Log("Variables saved to CSV file");
        }

        ///////////////// Call freecad python script to generate the feature in CAD /////////////////////////////////
        // Construct the command to execute
        string command = $"\"{freecadCmdPath}\" {scriptPath}";

        // Create process start info
        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.FileName = "cmd.exe";
        startInfo.Arguments = $"/C {command}";
        startInfo.RedirectStandardOutput = true;
        startInfo.RedirectStandardError = true;
        startInfo.UseShellExecute = false;
        startInfo.CreateNoWindow = true;

        // Start the process
        Process process = new Process();
        process.StartInfo = startInfo;
        process.Start();

        // Read output and errors
        string output = process.StandardOutput.ReadToEnd();
        string error = process.StandardError.ReadToEnd();

        // Log output and errors
        UnityEngine.Debug.Log("STDOUT: " + output);
        UnityEngine.Debug.LogError("STDERR: " + error);
        AssetDatabase.Refresh();
    }

    public void RemoveGrabbable()
    {
        GameObject voxelParentObject = GameObject.FindGameObjectWithTag("Removable_voxels");
        if (voxelParentObject != null)
        {
            Destroy(voxelParentObject.gameObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>());
            Destroy(voxelParentObject.gameObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.Transformers.XRGeneralGrabTransformer>());
            Destroy(voxelParentObject.gameObject.GetComponent<Rigidbody>());
        }
        else
        {
            UnityEngine.Debug.Log("No parent found on exit.");
        }
    }

}
