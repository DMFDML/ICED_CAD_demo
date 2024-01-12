using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateExtrudable2DShape : MonoBehaviour
{
    public float enlargementFactor = 0.1f; // Adjust the factor based on how much you want the square to enlarge

    private Vector3 initialPosition;
    private LineRenderer lineRenderer;

    private Color color = Color.red;

    public GameObject LinePrefab = null;
    public float lineWidth = 0.04f;
    public float minimumVertexDistance = 0.1f;
    private GameObject Lineobject = null;

    void Start()
    {
        // Save the initial position
        initialPosition = transform.position;
    }

    void Update()
    {
        // Check if the position has changed from the initial position
        if (transform.position != initialPosition)
        {
            // Calculate the distance between the current position and the initial position
            Vector3 distance = transform.position - initialPosition;

            // Enlarge the square based on the distance in each axis
            float scaleX = 1 + Mathf.Abs(distance.x) * enlargementFactor;
            float scaleY = 1 + Mathf.Abs(distance.y) * enlargementFactor;

            // Initialize the LineRenderer if not already initialized
            if (lineRenderer == null)
            {
                Lineobject = Instantiate(LinePrefab);
                lineRenderer = Lineobject.GetComponent<LineRenderer>();
                lineRenderer.positionCount = 5; // Five points to create a closed square
                lineRenderer.loop = true;
                lineRenderer.startColor = color;
                lineRenderer.endColor = color;
                // set width of the renderer
                lineRenderer.startWidth = lineWidth;
                lineRenderer.endWidth = lineWidth;
            }

            // Update the LineRenderer points to create a square
            lineRenderer.SetPosition(0, new Vector3(initialPosition.x, initialPosition.y, initialPosition.z));
            lineRenderer.SetPosition(1, new Vector3(initialPosition.x + distance.x, initialPosition.y, initialPosition.z));
            lineRenderer.SetPosition(2, new Vector3(initialPosition.x + distance.x, initialPosition.y, initialPosition.z + distance.z));
            lineRenderer.SetPosition(3, new Vector3(initialPosition.x, initialPosition.y, initialPosition.z + distance.z));
            lineRenderer.SetPosition(4, new Vector3(initialPosition.x, initialPosition.y, initialPosition.z));

            if (lineRenderer != null)
            {
                // Get the object's extent
                Vector3 objectExtent = lineRenderer.bounds.extents;

                // Set the size of the BoxCollider
                BoxCollider boxCollider = Lineobject.GetComponent<BoxCollider>();
                if (boxCollider != null)
                {
                    boxCollider.size = objectExtent * 2f; // Multiply by 2 to get the size
                    boxCollider.center = lineRenderer.bounds.center; // Set center relative to object's position
                }
                else
                {
                    Debug.LogWarning("BoxCollider component not found on the GameObject.");
                }
            }
            else
            {
                Debug.LogWarning("Renderer component not found on the GameObject.");
            }


        }
        else
        {
            // Destroy the LineRenderer if the GameObject is back at its initial position
            if (lineRenderer != null)
            {
                Destroy(lineRenderer);
            }
        }
    }
}




