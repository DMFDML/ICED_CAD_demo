using UnityEngine;
using TMPro;

public class TextOnGameObject : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;

    public void WriteInstructions()
    {
        // Set the text
        SetText("Press Trigger to End");
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
