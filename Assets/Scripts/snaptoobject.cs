//// It checks for Istrigger, if it 
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class snaptoobject : MonoBehaviour
//{
//    // The snap point on this object
//    public GameObject combined;

//    private void OnTriggerEnter(Collider namedcol)
//    {
//        Debug.Log("trigger entered " + namedcol.gameObject.transform);
//        namedcol.gameObject.transform.SetParent(combined.transform, true);
//    }
//}
using UnityEngine;

public class snaptoobject : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other GameObject has a Rigidbody (to ensure it's a movable object).
        Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
        if (otherRigidbody != null)
        {
            // Make the parent GameObject the new parent of the entered object.
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the other GameObject has a Rigidbody (to ensure it's a movable object).
        Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
        if (otherRigidbody != null)
        {
            // Remove the parent-child relationship between the parent and the exited object.
            other.transform.SetParent(null);
        }
    }
}

