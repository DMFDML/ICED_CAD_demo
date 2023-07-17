// It checks for Istrigger, if it 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class snaptoobject : MonoBehaviour
{
    // The snap point on this object
    public GameObject combined;

    private void OnTriggerEnter(Collider namedcol)
    {
        Debug.Log("trigger entered " +namedcol.name);
        namedcol.transform.SetParent(combined.transform, true);
    }
}

