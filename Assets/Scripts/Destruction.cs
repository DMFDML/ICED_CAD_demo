using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    //private Vector3[] BasechildLocations;

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
            voxelParent = new GameObject("Voxel Parent", typeof(BoxCollider));
            voxelParent.tag = tag;
            voxelParent.GetComponent<Transform>().position = transform.position;
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
                        vec = vec - new Vector3(cubeWidth/2 - cubeScale/2, cubeHeight/2  - cubeScale/2, cubeDepth/2  - cubeScale/2);

                        GameObject cubes = (GameObject)Instantiate(mesh, vec + new Vector3(x, y, z), voxelParent.GetComponent<Transform>().rotation);
                        cubes.transform.SetParent(voxelParent.GetComponent<Transform>());
                        cubes.gameObject.GetComponent<MeshRenderer>().material = gameObject.GetComponent<MeshRenderer>().material;
                    }
                }
            }
        }

        //// Get the number of children
        //int childCount = voxelParent.transform.childCount;

        //// Initialize the array to store child locations
        //BasechildLocations = new Vector3[childCount];

        //// Loop through each child and save its position
        //for (int i = 0; i < childCount; i++)
        //{
        //    Transform child = voxelParent.transform.GetChild(i);
        //    BasechildLocations[i] = child.localPosition;
        //}

        //// Print the first 10 values of childLocations
        //int printCount = Mathf.Min(10, BasechildLocations.Length);

        //for (int i = 0; i < printCount; i++)
        //{
        //    Debug.Log($"Base Child Location {i + 1}: {BasechildLocations[i]}");
        //}

        // Set the voxels into the original rotation of the block
        voxelParent.GetComponent<Transform>().Rotate(rotation, Space.Self);

    }
}
