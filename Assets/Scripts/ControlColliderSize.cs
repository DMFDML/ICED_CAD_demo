// To control size of sphere collider using joystick on XR controller, and also change color of the sphere when it hovers over the interactable object
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// Checks for button input on an input action
/// </summary>
public class ControlColliderSize : MonoBehaviour
{
    [Tooltip("Actions to check")]
    public InputAction action = null;

    // When the button is pressed
    public UnityEvent OnPress = new UnityEvent();

    // When the button is released
    public UnityEvent OnRelease = new UnityEvent();


    public float currentSize; // size of the SphereCollider
    public float minSize = 0.05f; // Maximum size of the SphereCollider
    public float maxSize = 1.0f; // Maximum size of the SphereCollider
    public float sizeChangeSpeed = 0.01f; // Speed at which the size changes

    public float sizedirection=2;
    public float updown;

    private SphereCollider sphereCollider;

    private void Awake()
    {
        action.started += Pressed;
        action.canceled += Released;
    }

    private void OnDestroy()
    {
        action.started -= Pressed;
        action.canceled -= Released;
    }

    private void OnEnable()
    {
        action.Enable();
    }

    private void OnDisable()
    {
        action.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        if (sphereCollider == null)
        {
            Debug.LogError("VRControllerSphereColliderResizer requires a SphereCollider component on the GameObject.");
            Destroy(this);
        }
    }

    private void Pressed(InputAction.CallbackContext context)
    {

        //Debug.Log(context);

        // Access the value using ReadValue<float>()
        Vector2 value = context.ReadValue<Vector2>();

        // Do something with the value
        //Debug.Log("Button Pressed! Value: " + value.y);


        if (value.y > 0)
        {
            //Debug.Log("Size increasing");
            // Calculate the new size based on joystick input
            float newSize = sphereCollider.radius + sizeChangeSpeed;

            // Clamp the size to the specified range
            newSize = Mathf.Clamp(newSize, minSize, maxSize);

            // Update the SphereCollider size
            sphereCollider.radius = newSize;

            // Get the first (and only) child
            Transform child = transform.GetChild(0);

            // Modify the scale of the child
            child.localScale = new Vector3(newSize * 2f, newSize * 2f, newSize * 2f);
        }
        else 
        {
            //Debug.Log("Size decreasing");

            // Calculate the new size based on joystick input
            float newSize = sphereCollider.radius - sizeChangeSpeed;

            // Clamp the size to the specified range
            newSize = Mathf.Clamp(newSize, minSize, maxSize);

            // Update the SphereCollider size
            sphereCollider.radius = newSize;

            // Get the first (and only) child
            Transform child = transform.GetChild(0);

            // Modify the scale of the child
            child.localScale = new Vector3(newSize * 2f, newSize * 2f, newSize * 2f);
        }

            //OnPress.Invoke();
    }

    private void Released(InputAction.CallbackContext context)
    {
        OnRelease.Invoke();
    }

    public void changecolorred()
    {
        // Get the first (and only) child
        Transform child = transform.GetChild(0);

        Renderer renderer = child.GetComponent<Renderer>();

        if (renderer != null)
        {
            Material material = renderer.material;

            // Change the albedo color to red
            material.color = Color.red;
        }
    }
    public void changecolorwhite()
    {
        // Get the first (and only) child
        Transform child = transform.GetChild(0);

        Renderer renderer = child.GetComponent<Renderer>();

        if (renderer != null)
        {
            Material material = renderer.material;

            // Change the albedo color to white
            material.color = Color.white;
        }
    }
}


