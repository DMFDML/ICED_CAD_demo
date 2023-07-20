using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePrimitive : MonoBehaviour
{
    public GameObject Prefab1;
    public GameObject Prefab2;
    private GameObject Prefab;
    
    private float typep = 0f;

    private float DirectionOfScale = 0f;
    private Vector3 scale;

    public void Start()
    {
        scale = new Vector3(0.1f, 0.1f, 0.1f);
    }

    public void SetPrimitiveType(float value)
    {
        typep = value;
        //Debug.Log(typep);
    }
    private void OnTriggerEnter(Collider namedcol)
    {
        if (typep== 0)
        {
            //Debug.Log("Value is 0 and prefab is Non-rigid");
            Prefab = Prefab1;
        }
        else
        {
            //Debug.Log("Value is 1 and prefab is Rigid");
            Prefab = Prefab2;
        }

        GameObject Newcube = Instantiate(Prefab);// CreatePrimitive(PrimitiveType.Cube);
        Newcube.transform.position = new Vector3(-1.53f, 0.95f, 1.40f);  //transform.position-new Vector3(0.2f,0,0);
        Newcube.transform.localScale = scale; //new Vector3(0.1f,0.1f,0.1f);
        //Newcube.AddComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>();
    }

    // Start is called before the first frame update
    //void Start()
    //{
    //    scale = new Vector3(0.1f,0.1f,0.1f);
    //}

    // Update is called once per frame
    public void setScale(float value)
    {
        if (DirectionOfScale == 0)
        {
            scale = new Vector3(0.1f + value, 0.1f, 0.1f);
            //Debug.Log(GameObject.transform.localScale.x);
        }
        else if (DirectionOfScale == 1)
        {
            scale = new Vector3(0.1f, 0.1f + value, 0.1f);
        }
        else if (DirectionOfScale == 2)
        {
            scale = new Vector3(0.1f, 0.1f, 0.1f + value);
        }
        else
        {
            scale = new Vector3(0.1f + value, 0.1f + value, 0.1f + value);
        }
    }

    public void SetDirection(float value)
    {
        DirectionOfScale = value;
        Debug.Log("Direction of Scale"+ DirectionOfScale);
    }

}
