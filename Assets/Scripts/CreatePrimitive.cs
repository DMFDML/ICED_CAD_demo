using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatePrimitive : MonoBehaviour
{
    public GameObject Prefab1;
    public GameObject Prefab2;
    private GameObject Prefab;
    private float typep = 1f;

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
        Newcube.transform.position = new Vector3(-0.95f, 0.89f, 1.545f);  //transform.position-new Vector3(0.2f,0,0);
        Newcube.transform.localScale = new Vector3(0.1f,0.1f,0.1f);
        //Newcube.AddComponent<UnityEngine.XR.Interaction.Toolkit.XRGrabInteractable>();
    }
}
