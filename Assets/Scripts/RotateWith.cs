using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWith : MonoBehaviour
{

    float speed;

    private void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.tag == "Environment")
        {
            return;
        } else if (col.gameObject.tag == "box")
        {
            speed = col.gameObject.GetComponent<WheelRotate>().speed;
            transform.Rotate(0, speed, 0);
        } else
        {
            col.gameObject.transform.SetParent(gameObject.transform);
        }
    }

}
