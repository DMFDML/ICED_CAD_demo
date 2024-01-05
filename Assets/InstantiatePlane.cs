using UnityEngine;

public class InstantiatePlane : MonoBehaviour
{
    public GameObject planePrefab; // Reference to the plane prefab you want to instantiate.

    public void CreatePlane()
    {
        Debug.Log("Pressed");
        // Get the rotation of the GameObject this script is attached to.
        Quaternion rotation = transform.rotation;

        // Round off the angle to the nearest value divisible by 5.
        float roundedAngle = Mathf.Round(rotation.eulerAngles.y / 5.0f) * 5.0f;

        // Create a new rotation with the rounded angle.
        Quaternion roundedRotation = Quaternion.Euler(0f, roundedAngle, 0f);
        // Instantiate the plane with the rounded rotation.
        Instantiate(planePrefab, transform.position, roundedRotation);
    }
}