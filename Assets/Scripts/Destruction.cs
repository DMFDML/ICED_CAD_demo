using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destruction : MonoBehaviour
{

    public GameObject mesh;
    GameObject voxelParent;

    float cubeWidth;
    float cubeHeight;
    float cubeDepth;

    public float cubeScalar = 10f;
    float cubeScale;

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
        this.gameObject.SetActive(false);     

        if (GetComponent<WheelRotate>() != null)
        {
            voxelParent = new GameObject("Voxel Parent", typeof(WheelRotate));
            voxelParent.GetComponent<Transform>().position = gameObject.transform.position;
            voxelParent.GetComponent<Transform>().rotation = gameObject.transform.rotation;
            voxelParent.GetComponent<WheelRotate>().speed = this.gameObject.GetComponent<WheelRotate>().speed;
        }
        else
        {
            voxelParent = new GameObject("Voxel Parent");
            voxelParent.GetComponent<Transform>().position = this.gameObject.transform.position;
            voxelParent.GetComponent<Transform>().rotation = this.gameObject.transform.rotation;
        }

        if (gameObject.CompareTag("box"))
        {
            for (float x = 0; x < cubeWidth; x += cubeScale)
            
            {//Debug.Log(x);
                for (float y = 0; y < cubeHeight; y+= cubeScale)
                {
                    for (float z = 0; z < cubeDepth; z += cubeScale)
                    {
                        Vector3 vec = transform.position;
                        // Debug.Log(vec);
                        vec = vec - new Vector3(cubeWidth/2 - cubeScale/2, cubeHeight/2  - cubeScale/2, cubeDepth/2  - cubeScale/2);
                        // Debug.Log(vec);

                        GameObject cubes = (GameObject)Instantiate(mesh, vec + new Vector3(x, y, z), Quaternion.identity);
                        cubes.transform.SetParent(voxelParent.GetComponent<Transform>());
                        cubes.gameObject.GetComponent<MeshRenderer>().material = gameObject.GetComponent<MeshRenderer>().material;
                    }
                }
            }
        }
    }
}
