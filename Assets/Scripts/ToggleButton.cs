using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    public GameObject targetObject;
    public Button toggleButton;

    private bool isObjectActive = true;

    private void Start()
    {
        toggleButton.onClick.AddListener(Toggle);
    }

    private void Toggle()
    {
        isObjectActive = !isObjectActive;
        targetObject.SetActive(isObjectActive);
    }
}

