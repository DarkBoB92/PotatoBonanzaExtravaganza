using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider1;
    [SerializeField] private Slider slider2;



    public void UpdateHealthBar(float currentValue, float maxValue)
    {
        slider1.value = currentValue / maxValue;   // Adjusts slider value depending on health in comparison to max health
        slider2.value = currentValue / maxValue;   // Adjusts slider value depending on health in comparison to max health
    }
}
