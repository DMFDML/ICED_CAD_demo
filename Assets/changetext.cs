// Script to change value of text in textmeshproUGUI component if it is already added to it
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class changetext : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    public void UpdateValue(string value)
    {
        textMeshPro.text = value;
    }
}

