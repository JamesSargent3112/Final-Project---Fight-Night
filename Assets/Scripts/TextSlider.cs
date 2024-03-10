using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextSlider : MonoBehaviour
{
    // Reference to the TextMeshProUGUI component that displays the slider value
    public TextMeshProUGUI numberText;
    private Slider slider; // Reference to the Slider component

    private void Start()
    {
        // Get the Slider component attached to this GameObject
        slider = GetComponent<Slider>();
        // Set the initial text of the numberText to the value of the Slider
        SetNumberText(slider.value);
    }

    // This method updates the text of the numberText with the provided value
    public void SetNumberText(float value)
    {
        numberText.text = value.ToString();
    }
}
