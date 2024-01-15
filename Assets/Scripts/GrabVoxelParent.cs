using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GrabVoxelParent : MonoBehaviour
{
    //public InputAction action = null;
    //public float handenteredinvoxel=1;

    //private void Awake()
    //{
    //    action.started += Pressed;
    //    action.canceled += Released;
    //}

    //private void OnDestroy()
    //{
    //    action.started -= Pressed;
    //    action.canceled -= Released;
    //}

    //private void Pressed(InputAction.CallbackContext context)
    //{
    //    if (handenteredinvoxel == 0)
    //    {
    //        GameObject handObject = GameObject.FindGameObjectWithTag("hand");
    //        // Get the parent of the current GameObject
    //        Transform voxelChild = transform.parent;
    //        // Check if the "voxel" child is found
    //        if (voxelChild != null)
    //        {
    //            // Set the "hand" GameObject as the parent of the "voxel" child
    //            voxelChild.parent = handObject.transform;

    //            Debug.Log("Set " + handObject.name + " as the parent of " + voxelChild.name);
    //        }
    //        else
    //        {
    //            Debug.LogWarning("Child named 'voxel' not found.");
    //        }
    //    }
    //}

    //private void Released(InputAction.CallbackContext context)
    //{
    //    GameObject handObject = GameObject.FindGameObjectWithTag("hand");
    //    // Get the parent of the current GameObject
    //    Transform voxelChild = transform.parent;
    //    // Check if the "voxel" child is found
    //    // Check if the "voxel" child is found
    //    if (voxelChild != null)
    //    {
    //        // Set the parent of the "voxel" child to null, effectively removing the current parent
    //        voxelChild.parent = null;

    //        Debug.Log("Removed parent of " + voxelChild.name);
    //    }
    //    else
    //    {
    //        Debug.LogWarning("Child named 'voxel' not found.");
    //    }
    //}

    void OnTriggerEnter(Collider other)
    {
        // Check if the entering GameObject has the "hand" tag
        if (other.CompareTag("hand"))
        {
            // Get the parent of the current GameObject
            Transform voxelparent= transform.parent;

            // Print the name of the parent
            if (voxelparent != null)
            {
                Debug.Log("Trigger Entered Parent Name: " + voxelparent.name);
                //handenteredinvoxel = 0;
                
                //Set the "hand" GameObject as the parent of the "voxelparent"
                voxelparent.gameObject.AddComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>();

                // Set the Rigidbody properties if the Interactable component has a Rigidbody
                Rigidbody rb = voxelparent.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // Turn off gravity
                    rb.useGravity = false;

                    // Make it kinematic
                    rb.isKinematic = true;
                }

                //voxelparent.parent = other.transform;
            }
            else
            {
                Debug.Log("No parent found.");
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the entering GameObject has the "hand" tag
        if (other.CompareTag("hand"))
        {
            // Get the parent of the current GameObject
            Transform voxelparent = transform.parent;

            // Print the name of the parent
            if (voxelparent != null)
            {
                Debug.Log("Trigger exit Parent Name: " + voxelparent.name);
                //handenteredinvoxel = 0;
                //Set the "hand" GameObject as the parent of the "voxel" child
                Destroy(voxelparent.gameObject.GetComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>());
                //voxelparent.parent = other.transform;
            }
            else
            {
                Debug.Log("No parent found.");
            }
        }
    }

    public void LeaveVoxelParent()
    {
        GameObject handObject = GameObject.FindGameObjectWithTag("hand");
        // Get the parent of the current GameObject
        Transform voxelChild = transform.parent;
        // Check if the "voxel" child is found
        // Check if the "voxel" child is found
        if (voxelChild != null)
        {
            // Set the parent of the "voxel" child to null, effectively removing the current parent
            voxelChild.parent = null;

            Debug.Log("Removed parent of " + voxelChild.name);
        }
        else
        {
            Debug.LogWarning("Child named 'voxel' not found.");
        }
    }
}
