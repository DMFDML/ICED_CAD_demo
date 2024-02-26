// Script to add instantiation capability and enlarge it as per the distance from intial positon (when trigger is pressed) to final position
using UnityEngine;

public class EnlargeCube : MonoBehaviour
{
    public float enlargementFactor = 0.1f; // Adjust the factor based on how much you want the cube to enlarge
    public GameObject Prefab;

    private Vector3 initialPosition;
    private Vector3 basePosition;
    private Vector3 initialScale;
    private GameObject cube;
    private float startcubeflag = 1f;
    void Start()
    {
        // Save the base position where the grabbed shape will return after leaving it
        basePosition = transform.position;
        //initialScale = transform.localScale;
        //Debug.Log(initialScale);
    }

    void Update()
    {
        if (startcubeflag==0f)
        {
            // Check if the position has changed from the initial position
            if (transform.position != initialPosition)
            {
                // Instantiate the cube if it hasn't been instantiated yet
                if (cube == null)
                {
                    Debug.Log(cube);
                    // Instantiate a prefab at the initial position
                    //cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    cube = Instantiate(Prefab);// CreatePrimitive(PrimitiveType.Cube);
                    cube.transform.position = initialPosition;
                    cube.transform.localScale = initialScale;
                    //Debug.Log(cube.transform.localScale);
                }

                // Calculate the distance between the current position and the initial position
                Vector3 distance = transform.position - initialPosition;

                // Enlarge the cube based on the distance in each axis
                float scaleX = initialScale.x + Mathf.Abs(distance.x) * enlargementFactor;
                float scaleY = initialScale.y + Mathf.Abs(distance.y) * enlargementFactor;
                float scaleZ = initialScale.z + Mathf.Abs(distance.z) * enlargementFactor;

                cube.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
            }
            else
            {
                // Destroy the cube if it exists and the GameObject is back at its initial position
                if (cube != null)
                {
                    Destroy(cube);
                }
            }
        }
    }

    public void startcube()
    {
        // Save the initial position
        initialPosition = transform.position;
        initialScale = new Vector3(0.2f,0.2f,0.2f);
        cube = Instantiate(Prefab);// CreatePrimitive(PrimitiveType.Cube);
        cube.transform.position = initialPosition;
        cube.transform.localScale = initialScale;
        startcubeflag = 0f;
    }

    public void endcube()
    {
        startcubeflag = 1f;
    }

    public void backtobase()
    {
        transform.position = basePosition;
    }
}
