using UnityEngine;

/// <summary>
/// Removes all objects in a scene using a particular tag
/// </summary>
public class RemoveObjectsWithTag : MonoBehaviour
{
    public void RemoveObjects(string tag)
    {
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
        Debug.Log(taggedObjects);
        foreach (GameObject targetObject in taggedObjects)
            Destroy(targetObject);  
    }
}
