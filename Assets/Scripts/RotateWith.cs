using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateWith : MonoBehaviour
{

    float speed;

    private void OnCollisionEnter(Collision col) 
    {
        speed = gameObject.GetComponent<WheelRotate>().speed;
        col.gameObject.transform.Rotate(0, speed, 0);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
