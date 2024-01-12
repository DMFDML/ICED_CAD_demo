using UnityEngine;
/// <summary>
/// This script creates a trail at the location of a gameobject with a particular width and color.
/// </summary>

public class CreatePolyline : MonoBehaviour
{
    private Color color = Color.red;
    private LineRenderer Line;
    public GameObject LinePrefab = null;
    public float lineWidth = 0.04f;
    public float minimumVertexDistance = 0.1f;
    private GameObject Lineobject = null;
    //public GameObject combined;
    private bool isLineStarted;
    private float newLineStarted = 1f;

    public string tag = "PolyLine"; // The desired tag for the instantiated GameObject

    public void StartTrail()
    {
        if (newLineStarted == 1f)
        {
            Lineobject = Instantiate(LinePrefab);
            //Lineobject.tag = tag;
            //Debug.Log(combined);
            //Lineobject.transform.SetParent(combined.transform, true);
            // set the color of the line
            Line = Lineobject.GetComponent<LineRenderer>();

            //Debug.Log(color);
            Line.startColor = color;
            Line.endColor = color;

            //Line.startColor = Color.red;
            //Line.endColor = Color.red;

            // set width of the renderer
            Line.startWidth = lineWidth;
            Line.endWidth = lineWidth;

            Line.positionCount=0;

            //Debug.Log("Start Line");
            Vector3 mousePos = transform.position;

            Line.positionCount = 2;
            Line.SetPosition(Line.positionCount, mousePos);
            Line.SetPosition(0, mousePos);
            Line.SetPosition(1, mousePos);
            newLineStarted = 2f;
            Line.positionCount = 3;
            isLineStarted = true;
        }
        else if (newLineStarted == 2f)
        {
            Line.positionCount++;
        }
    }

    public void Update()
    {
        //Debug.Log("Drawing Line");
        if (isLineStarted)
        {
            //Debug.Log("checking");
            Vector3 currentPos = transform.position;
            //Debug.Log(currentPos);
            float distance = Vector3.Distance(currentPos, Line.GetPosition(Line.positionCount - 2));
            if (distance > minimumVertexDistance)
            {
                Debug.Log(Line.positionCount);
                UpdateLine();
            }
        }
    }

    public void EndTrail()
    {
        if (newLineStarted == 2f)
        {
            //Debug.Log(Line.GetPosition());
            Lineobject.GetComponent<BoxCollider>().center = transform.position;
            //Lineobject.GetComponent<BoxCollider>().size = transform.position;

            isLineStarted = false;
        }
    }

    public void SetWidth(float value)
    {
        lineWidth = value;
    }

    public void SetColor(Color value)
    {
        color = value;
    }

    private void UpdateLine()
    {
        //Line.positionCount++;
        Line.SetPosition(Line.positionCount-1, transform.position);
    }
}