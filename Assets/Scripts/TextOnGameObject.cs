// Script to add text to the textmeshproUGUI on calling function writeinstructions, and remove it on calling removetext
// Add textmeshproUI to any gameobject for which you want to use these functions and then drag that gameobject on xrgrabinteractor "activate" interaction event
// or on "on button press" script, select writeinstructions function from the dropdown menu.
using UnityEngine;
using TMPro;

public class TextOnGameObject : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    //public string text;

    public void WriteInstructions(string text)
    {
        // Set the text
        //SetText("Press Trigger to End");
        SetText(text);
    }

    void SetText(string newText)
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = newText;
        }
    }

    public void RemoveText()
    {
        // Destroy the GameObject (including this script)
        SetText("");
    }
}
