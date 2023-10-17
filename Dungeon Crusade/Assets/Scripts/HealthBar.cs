using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Make sure to include this for Slider

public class HealthBar : MonoBehaviour
{
    public Slider slider;

    // Ensure that the slider reference is properly set in the Inspector.
    // You can do this by dragging and dropping the Slider component onto the script field.


    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
        
    }
    public void SetHealth(int health)
    {
        // Ensure that the slider's value remains within a valid range (usually 0 to 1).
        slider.value = health;
    }
}
