using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Destruction : MonoBehaviour
{
    public GameObject mesh;
    GameObject voxelParent;
    Vector3 rotation;

    float cubeWidth;
    float cubeHeight;
    float cubeDepth;

    public float cubeScalar = 10f;
    float cubeScale;
    public string tag = "Removable_voxels";

    private Vector3[] BasechildLocations;
    // File path to save the CSV file
    private string filePath;

    // Start is called before the first frame update
    void Start()
    {
        cubeWidth = transform.localScale.x;
        cubeHeight = transform.localScale.y;
        cubeDepth = transform.localScale.z;

        cubeScale = Mathf.Max(cubeDepth, cubeHeight, cubeWidth) / cubeScalar;

        // gameObject.GetComponent<MeshRenderer>().enabled = false;
        mesh.gameObject.GetComponent<Transform>().localScale = new Vector3(cubeScale, cubeScale, cubeScale);
    }

    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.tag == "Projectile")
        {
            CreateCube();
        }

        if (collision.gameObject.tag == "Tool")
        {
            CreateCube();
        }
    }

    //Newly added code for checking type of collider and remove all voxel outside it at start
    bool IsPositionInsideMeshCollider(Vector3 position)
    {
        // Get the collider attached to the current object
        Collider collider = voxelParent.GetComponent<Collider>();
        Debug.Log(collider);

        if (collider != null)
        {
            // Convert the world position to local position
            Vector3 localPosition = collider.transform.InverseTransformPoint(position);

            // Check based on collider type
            if (collider is SphereCollider)
            {
                // SphereCollider specific logic
                SphereCollider sphereCollider = (SphereCollider)collider;
                float distance = Vector3.Distance(localPosition, sphereCollider.center);
                return distance <= sphereCollider.radius;
            }
            else if (collider is CapsuleCollider)
            {
                // CapsuleCollider specific logic
                CapsuleCollider capsuleCollider = (CapsuleCollider)collider;
                return IsPositionInsideCapsule(localPosition, capsuleCollider);
            }
            else if (collider is BoxCollider)
            {
                // Use the bounds of the collider to check if the point is inside
                if (collider.bounds.Contains(localPosition))
                {
                    return true; // Position is inside at least one collider
                }
            }
            else
            {
                Debug.LogWarning("Collider type not supported for checking inside.");
            }
        }

        // Position is outside all colliders
        return false;
    }

    bool IsPositionInsideCapsule(Vector3 localPosition, CapsuleCollider capsuleCollider)
    {
        // CapsuleCollider specific logic
        // Implementation to check if the localPosition is inside the capsule
        // You need to customize this logic based on the shape of the capsule

        // Example: Check if the localPosition is within the capsule's height
        float halfHeight = capsuleCollider.height * 0.5f;
        float distanceToCenterY = Mathf.Abs(localPosition.y - capsuleCollider.center.y);

        return distanceToCenterY <= halfHeight;
    }
    ///////////////////////////// End of new code //////////////////////////////////////////////
    

    void CreateCube()
    {

        // Get the rotation of the block being changed
        rotation = transform.localRotation.eulerAngles;

        if (GetComponent<WheelRotate>() != null)
        {
            voxelParent = new GameObject("Voxel Parent", typeof(WheelRotate));
            voxelParent.tag = tag;
            voxelParent.GetComponent<Transform>().position = transform.position;
            voxelParent.GetComponent<WheelRotate>().speed = this.gameObject.GetComponent<WheelRotate>().speed;
        }
        else
        {
            //// New Code ////
            // Get the collider attached to the current object
            Collider collider = GetComponent<Collider>();

            if (collider != null)
            {
                // Get the type of the collider
                System.Type colliderType = collider.GetType();

                // Create a new GameObject
                voxelParent = new GameObject("Voxel Parent");

                // Add the same type of collider to the new GameObject
                Collider newCollider = voxelParent.AddComponent(colliderType) as Collider;

                if (newCollider != null)
                {
                    // Position the new GameObject at the same position as the current object
                    voxelParent.transform.position = transform.position;

                    Debug.Log("Added collider of type " + colliderType.Name + " to the new GameObject.");
                }
                else
                {
                    Debug.LogError("Failed to add collider to the new GameObject.");
                }
            }
            else
            {
                Debug.LogError("No Collider attached to the GameObject!");
            }
            //////////  New code end /////////////////////////////////////////////

            // Get the collider attached to the current object
            //voxelParent = new GameObject("Voxel Parent", typeof(BoxCollider));
            voxelParent.tag = tag;
            voxelParent.GetComponent<Transform>().position = transform.position;
            voxelParent.GetComponent<Transform>().localScale= transform.localScale;
        }

        //turn the block off
        this.gameObject.SetActive(false);

        // if the block is one that can be turned into voxels, do it
        if (gameObject.CompareTag("box"))
        {
            for (float x = 0; x < cubeWidth; x += cubeScale)
            
            {//Debug.Log(x);
                for (float y = 0; y < cubeHeight; y+= cubeScale)
                {
                    for (float z = 0; z < cubeDepth; z += cubeScale)
                    {
                        Vector3 vec = transform.position;
                        vec = vec - new Vector3(cubeWidth / 2 - cubeScale / 2, cubeHeight / 2 - cubeScale / 2, cubeDepth / 2 - cubeScale / 2);
                        //Debug.Log(vec + new Vector3(x, y, z));
                        // Check if the position is inside the mesh collider
                        if (IsPositionInsideMeshCollider(vec + new Vector3(x, y, z))) // added to check the extents of collider and remove outside voxels,
                                                                                      // currently checks for box, sphere and capsule colliders. uses above functions
                        {
                            //Debug.Log("Yes, the position is inside the mesh collider!");
                            GameObject cubes = (GameObject)Instantiate(mesh, vec + new Vector3(x, y, z), voxelParent.GetComponent<Transform>().rotation);
                            cubes.AddComponent<GrabVoxelParent>(); // Script added at the time of creation to enable addition of xrgrabinteractable to the voxel parent when a hand collides with it
                            cubes.transform.SetParent(voxelParent.GetComponent<Transform>());
                            cubes.gameObject.GetComponent<MeshRenderer>().material = gameObject.GetComponent<MeshRenderer>().material;
                        }
                        else
                        {
                            Debug.Log("No, the position is outside the mesh collider.");
                        }

                        
                    }
                }
            }
        }

        // Get the number of children
        int childCount = voxelParent.transform.childCount;

        // Initialize the array to store child locations
        BasechildLocations = new Vector3[childCount];

        // loop through each child and save its position
        for (int i = 0; i < childCount; i++)
        {
            Transform child = voxelParent.transform.GetChild(i);
            BasechildLocations[i] = child.localPosition;
        }

        // print the first 10 values of childlocations
        int printCount = Mathf.Min(10, BasechildLocations.Length);

        for (int i = 0; i < printCount; i++)
        {
            Debug.Log($"Base child location {i + 1}: {BasechildLocations[i]}");
        }

        // set the voxels into the original rotation of the block
        voxelParent.GetComponent<Transform>().Rotate(rotation, Space.Self);

        // Save Raw Stock Voxel Model
        filePath = Application.dataPath + $"/features/feature 0.csv";
        // Create or overwrite the file
        using (StreamWriter sw = new StreamWriter(filePath))
        {
            // Write header
            //sw.WriteLine("Child Locations");

            // Write data
            for (int i = 0; i < BasechildLocations.Length; i++)
            {
                //Debug.Log($"{childLocations[i].x}, {childLocations[i].y}, {childLocations[i].z}");
                sw.WriteLine($"{BasechildLocations[i].x}, {BasechildLocations[i].y}, {BasechildLocations[i].z}");
            }
            Debug.Log("Base Variables saved to CSV file");
        }
    }
}